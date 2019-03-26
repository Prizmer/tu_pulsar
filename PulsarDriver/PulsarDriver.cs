using System;
using System.Collections.Generic;

using PollingLibraries.LibPorts;
using Drivers.LibMeter;

namespace Drivers.PulsarDriver
{
    public class PulsarDriver : CMeter, IMeter
    {
        Dictionary<byte, string> m_dictDataTypes = new Dictionary<byte, string>();
        List<byte> m_listTypesForRead = new List<byte>();

        private byte[] m_cmd;
        private byte m_length_cmd = 0;
        private byte m_max_canals = 16;

        public void SetTypesForRead(List<byte> types)
        {
            for (int i = 0; i < types.Count; i++)
            {
                if (m_dictDataTypes.ContainsKey(types[i]))
                {
                    m_listTypesForRead.Add(types[i]);
                }
            }
        }

        private void ClearTypesForRead()
        {
            m_listTypesForRead.Clear();
        }

        private PulsarMeterTypes? meterType = null;
        private bool GetValueFromBytesByMeterType(byte[] data, int startIndexMult, out double val, bool archives = false)
        {
            val = -1f;
            if (meterType == null)
            {
                string tmpMeterType = "";
                if (!this.ReadMeterType(ref tmpMeterType))
                    meterType = PulsarMeterTypes.kompaktniy_teplo_v3;
            }

            WriteToLog("GetValueFromBytesByMeterType MeterType: " + meterType.Value);

            try
            { 
                if (meterType == PulsarMeterTypes.pulsarM || meterType == PulsarMeterTypes.voda_v6 || meterType == PulsarMeterTypes.voda_v11)
                { 
                    //согласно документации
                    if (!archives) val = (double)BitConverter.ToInt32(data, 6 + startIndexMult * 4) / 1000;
                    else val = (double)BitConverter.ToInt32(data, 0) / 1000;
                }
                else if (meterType == PulsarMeterTypes.voda_rs485)
                {
                    if (!archives) val = BitConverter.ToSingle(data, 6 + startIndexMult * 4);
                    else val = BitConverter.ToSingle(data, 0); 
                }
                else if (meterType == PulsarMeterTypes.kompaktniy_teplo_v3 || meterType == PulsarMeterTypes.kompaktniy_teplo_v4)
                {
                   if (!archives) val = BitConverter.ToSingle(data, 6 + startIndexMult * 4);
                   else val = BitConverter.ToSingle(data, 0);

                }
            }catch(Exception ex)
            {
                WriteToLog("GetValueFromBytesByMeterType: возможно выбран параметр не поддерживаемый данной версией счетчика: " + ex);
                return false;
            }

            val = Math.Round(val, 7);

            if (val > -1) return true;
            else return false;
        }


        public bool ReadCurrentValues(ref Values values)
        {
            m_length_cmd = 0;
            Byte[] in_buffer = new Byte[255];

            ushort rnd = 0x50b9;
            byte func = 0;

            uint channel_mask = 0;
            byte out_packet_length = 0;
            int bytes_to_read = -1;

            func = 1;
            out_packet_length = 14;

            // битовая маска каналов
            for (int i = 0; i < m_listTypesForRead.Count; i++)
            {
                uint tmp = 1;
                tmp = tmp << (m_listTypesForRead[i] - 1);
                channel_mask |= tmp;
            }

            // адрес
            byte[] adr = new byte[4];
            Int2BCD((int)m_address, adr);

            byte[] parameter = BitConverter.GetBytes(channel_mask);
            byte[] random = BitConverter.GetBytes(rnd);

            m_cmd = new byte[out_packet_length];

            // формируем команду 
            // адрес
            for (int i = 0; i < adr.Length; i++)
            {
                m_cmd[m_length_cmd++] = adr[i];
            }
            // номер функции
            m_cmd[m_length_cmd++] = func;
            // общая длина пакета
            m_cmd[m_length_cmd++] = out_packet_length;
            // параметры
            for (int i = 0; i < parameter.Length; i++)
            {
                m_cmd[m_length_cmd++] = parameter[i];
            }

            // ID
            for (int i = 0; i < random.Length; i++)
            {
                m_cmd[m_length_cmd++] = random[i];
            }

            // CRC16
            byte[] crc16 = CRC16(m_cmd, m_length_cmd);
            for (int i = 0; i < crc16.Length; i++)
            {
                m_cmd[m_length_cmd++] = crc16[i];
            }

            // байт для чтения
            bytes_to_read = 10 + 8 * m_listTypesForRead.Count;

        //WriteToLog("m_length_cmd=" + m_length_cmd.ToString());
        //WriteToLog("bytes_to_read=" + bytes_to_read.ToString());

        WriteToLog("ReadCurrentValues: Исходящие: " + BitConverter.ToString(m_cmd));
            if (m_vport.WriteReadData(FindPacketSignature, m_cmd, ref in_buffer, m_length_cmd, -1) > 0)
            {
            WriteToLog("ReadCurrentValues: Входящие: " + BitConverter.ToString(in_buffer));
            //WriteToLog("WriteReadData");
            bool find_header = true;

            // длина пакета 
            byte packet_length = 0;


            if (!CheckReceivedBytes(in_buffer, "ReadCurrentValues"))
                return false;       

            // проверка заголовка пакета
            for (int i = 0; i < 5; i++)
            {
                if (m_cmd[i] != in_buffer[i])
                {
                    find_header = false;
                }
            }

            if (m_listTypesForRead.Count == 0)
            {
                WriteToLog("ReadCurrentValues: cписок m_listTypesForRead пуст");
            }

            if (find_header)
            {
                //WriteToLog("find_header");
                packet_length = in_buffer[5];

                // проверка CRC
                crc16 = CRC16(in_buffer, packet_length - 2);

                if (in_buffer[packet_length - 2] == crc16[0] && in_buffer[packet_length - 1] == crc16[1])
                {
                    // проверка ID
                    if (m_cmd[out_packet_length - 4] == in_buffer[packet_length - 4] && m_cmd[out_packet_length - 3] == in_buffer[packet_length - 3])
                    {
                       values.listRV = new List<RecordValue>();

                        for (int i = 0; i < m_listTypesForRead.Count; i++)
                        {
                            RecordValue recordValue;
                            recordValue.type = m_listTypesForRead[i];
                            recordValue.fine_state = false;
                            recordValue.value = -1;

                            if (!GetValueFromBytesByMeterType(in_buffer, i, out recordValue.value))
                                return false;

                            recordValue.fine_state = true;

                            values.listRV.Add(recordValue);
                            WriteToLog("Значение: " + recordValue.value);
                        }

                        return true;
                    }
                    else
                    {
                        WriteToLog("ReadCurrentValues: неверный id");
                    }
                }else
                {
                    WriteToLog("ReadCurrentValues: неверный CRC");
                }
            }
            else
            {
                WriteToLog("ReadCurrentValues: первые 5 байт не равняются отправленной команде");
            }
            }

            return false;
        }

        bool CheckReceivedBytes(byte[] data, string methodName = "")
        {
            try
            {
                if (data.Length == 0)
                {
                    WriteToLog(methodName + ": получено 0 байт");
                    WriteToLog(methodName + ": прекращает работу");
                    return false;
                }

                if (data.Length < 6)
                {
                    WriteToLog(methodName + ": получено мало байт - " + data.Length);
                    WriteToLog(methodName + ": получено: " + BitConverter.ToString(data));
                    WriteToLog(methodName + ": прекращает работу");
                    return false;
                }

                if (data.Length > 5 && data.Length < (data[5]))
                {
                    WriteToLog(methodName + ": сообщение должно быть минимум - " + data[5] + " байт, а пришло лишь " + data.Length);
                    WriteToLog(methodName + ": получено: " + BitConverter.ToString(data));
                    WriteToLog(methodName + ": прекращает работу");
                    return false;
                }

                // всегда надо проверять CRC
                int packet_length = data.Length;
                if (data.Length >= packet_length)
                {
                    byte[] crc16 = CRC16(data, packet_length - 2);
                    if (data[packet_length - 2] != crc16[0] || data[packet_length - 1] != crc16[1])
                    {
                        WriteToLog(methodName + ": ошибка проверки CRC, расчитанная: " + BitConverter.ToString(crc16));
                        WriteToLog(methodName + ": получено: " + BitConverter.ToString(data));
                        WriteToLog(methodName + ": прекращает работу");
                        return false;
                    }
                }
                else
                {
                    WriteToLog(methodName + ": кол-во байт не соответствует пришедшему в параметре L - " + data.Length);
                    WriteToLog(methodName + ": получено: " + BitConverter.ToString(data));
                    WriteToLog(methodName + ": прекращает работу");
                    return false;
                }

            } catch (Exception ex)
            {
                WriteToLog(methodName + ": друг мой, сработал трай-кетч, ты где-то не проверил входит ли индекс в массив?!\n" + ex.ToString());
                return false;
            }



            return true;

        }

        public bool ReadMonthlyValues(byte month, ushort year, ref Values values)
        {
            return ReadArchive(TypeDataPulsar.Monthly, new DateTime(year, month, 1), ref values);
        }

        public bool ReadDailyValues(byte day, byte month, ushort year, ref Values values)
        {
            return ReadArchive(TypeDataPulsar.Daily, new DateTime(year, month, day), ref values);
        }

        public bool WriteValue(double value, int channel = 1, bool isDouble = true)
        {
            // адрес
            byte[] addrArr = new byte[4];
            Int2BCD((int)m_address, addrArr);
            byte func = 0x03;

            byte[] val = BitConverter.GetBytes(value);

            Int32 channelMask = 0;
            channelMask = 1 << (channel - 1);
            byte[] channelMaskBytes = BitConverter.GetBytes(channelMask);

            byte packetLength = 0x16;
            List<byte> cmdList = new List<byte>();
            cmdList.AddRange(addrArr);
            cmdList.Add(func);
            cmdList.Add(packetLength);
            cmdList.AddRange(channelMaskBytes);
            cmdList.AddRange(val);
            ushort rnd = 0x50b9;
            byte[] random = BitConverter.GetBytes(rnd);
            cmdList.AddRange(random);

            byte[] crc16 = CRC16(cmdList.ToArray(), cmdList.Count);
            cmdList.AddRange(crc16);


            //send to meter
            byte[] in_buffer = new byte[1];
            if (m_vport.WriteReadData(FindPacketSignature, cmdList.ToArray(), ref in_buffer, cmdList.Count, -1) > 0)
            {
                return true;
            }

            return false;
        }

        private bool ReadArchive(TypeDataPulsar type, DateTime date, ref Values values)
        {
            values.listRV = new List<RecordValue>();
            m_length_cmd = 0;
            Byte[] in_buffer = new Byte[255];

            ushort rnd = 0x50b9;
            byte func = 0;

            byte out_packet_length = 0;
            int bytes_to_read = -1;

            func = 6;
            out_packet_length = 28;

            // адрес
            byte[] adr = new byte[4];
            Int2BCD((int)m_address, adr);

            m_cmd = new byte[out_packet_length];

            //WriteToLog("ReadArchive: m_listTypesForRead.Count: " + m_listTypesForRead.Count);

            for (int t = 0; t < m_listTypesForRead.Count; t++)
            {
                m_length_cmd = 0;

                // битовая маска каналов
                uint channel_mask = 0;
                uint tmp = 1;
                tmp = tmp << (m_listTypesForRead[t] - 1);
                channel_mask |= tmp;

                byte[] parameter = BitConverter.GetBytes(channel_mask);
                byte[] random = BitConverter.GetBytes(rnd);

                // формируем команду 
                // адрес
                for (int i = 0; i < adr.Length; i++)
                {
                    m_cmd[m_length_cmd++] = adr[i];
                }
                // номер функции
                m_cmd[m_length_cmd++] = func;
                // общая длина пакета
                m_cmd[m_length_cmd++] = out_packet_length;
                // параметры
                for (int i = 0; i < parameter.Length; i++)
                {
                    m_cmd[m_length_cmd++] = parameter[i];
                }

                bytes_to_read = 10 + 2 + 2 + 6 + 4 * 1;

                // тип архива
                m_cmd[m_length_cmd++] = Convert.ToByte(type);
                m_cmd[m_length_cmd++] = 0;
                // дата - начало
                m_cmd[m_length_cmd++] = Convert.ToByte(date.Year - 2000);
                m_cmd[m_length_cmd++] = Convert.ToByte(date.Month);
                m_cmd[m_length_cmd++] = Convert.ToByte(date.Day);
                m_cmd[m_length_cmd++] = 0;
                m_cmd[m_length_cmd++] = 0;
                m_cmd[m_length_cmd++] = 0;
                // дата - конец
                m_cmd[m_length_cmd++] = Convert.ToByte(date.Year - 2000);
                m_cmd[m_length_cmd++] = Convert.ToByte(date.Month);
                m_cmd[m_length_cmd++] = Convert.ToByte(date.Day);
                m_cmd[m_length_cmd++] = 0;
                m_cmd[m_length_cmd++] = 0;
                m_cmd[m_length_cmd++] = 0;

                // ID
                for (int i = 0; i < random.Length; i++)
                {
                    m_cmd[m_length_cmd++] = random[i];
                }

                // CRC16
                byte[] crc16 = CRC16(m_cmd, m_length_cmd);
                for (int i = 0; i < crc16.Length; i++)
                {
                    m_cmd[m_length_cmd++] = crc16[i];
                }

                if (m_vport.WriteReadData(FindPacketSignature, m_cmd, ref in_buffer, m_length_cmd, -1) > 0)
                {
                    bool find_header = true;

                    // длина пакета 
                    byte packet_length = 0;

                    if (!CheckReceivedBytes(in_buffer, "ReadArchive"))
                        return false;

                    // проверка заголовка пакета
                    for (int i = 0; i < 5; i++)
                    {
                        if (m_cmd[i] != in_buffer[i])
                        {
                            find_header = false;
                        }
                    }

                    if (find_header)
                    {
                        packet_length = in_buffer[5];

                        // проверка CRC
                        crc16 = CRC16(in_buffer, packet_length - 2);

                        if (in_buffer[packet_length - 2] == crc16[0] && in_buffer[packet_length - 1] == crc16[1])
                        {
                            // проверка ID
                            if (m_cmd[out_packet_length - 4] == in_buffer[packet_length - 4] && m_cmd[out_packet_length - 3] == in_buffer[packet_length - 3])
                            {
                                RecordValue recordValue;
                                recordValue.type = m_listTypesForRead[t];
                                recordValue.fine_state = false;
                                recordValue.value = -1;

                                int data_elements_count = (packet_length - 10 - 4 - 6) / 4;

                                WriteToLog("ReadArchive: type archive=" + type.ToString() + "; data_elements_count=" + data_elements_count.ToString() + "; Date=" + date.ToShortDateString());
                                WriteToLog("ReadArchive: принятое: " + BitConverter.ToString(in_buffer));

                                if (data_elements_count > 0)
                                {
                                    int iyear = in_buffer[6 + 4] + 2000;
                                    int imon = in_buffer[6 + 4 + 1];
                                    int iday = in_buffer[6 + 4 + 2];

                                    //WriteToLog("dt_begin=" + date.ToString() + "; idate=" + iday.ToString() + "." + imon.ToString() + "." + iyear.ToString());

                                    if (date.Year == iyear && date.Month == imon && date.Day == iday)
                                    {
                                        data_elements_count = 1;

                                        for (int d = 0; d < data_elements_count; d++)
                                        {
                                            byte[] temp_buff = new byte[4];
                                            for (int b = 0; b < 4; b++)
                                            {
                                                temp_buff[b] = in_buffer[6 + 4 + 6 + d * 4 + b];
                                            }

                                            if ((temp_buff[0] == 0xF0 || temp_buff[0] == 0xF1) && temp_buff[1] == 0xFF && temp_buff[2] == 0xFF && temp_buff[3] == 0xFF)
                                            {
                                                WriteToLog("ReadArchives: tempBuf" + BitConverter.ToString(temp_buff) + " - в данном диапазоне дат архивации не проводилось или запрашиваемый период более глубины архива согласно документации.");
                                                WriteToLog("ReadArchives: дата: " + date);
                                                recordValue.value = -1;
                                                return false;
                                            }
                                            else
                                            {
                                                WriteToLog("ReadArchives: " + BitConverter.ToString(temp_buff));

                                                if (!GetValueFromBytesByMeterType(temp_buff, 0, out recordValue.value, true)){
                                                    return false;
                                                }
                                  

                                                recordValue.fine_state = true;
                                            }

                                            //WriteToLog("value=" + recordValue.value.ToString());

                                            
                                            values.listRV.Add(recordValue);
                                        }
                                    } else
                                    {
                                        WriteToLog(String.Format("ReadArchive: дата архива  {0} не совпадает с требуемой {1}",
                                           iyear + "-" + imon + "-" + iday, date));
                                    }
                                }
                                else
                                {
                                    WriteToLog(String.Format("ReadArchive: data_elements_count == 0"));
                                }
                            }
                            else
                            {
                                WriteToLog(String.Format("ReadArchive: не совпадает ID в команде {0} в ответе {1}",
                                    BitConverter.ToString(m_cmd), BitConverter.ToString(in_buffer)));
                            }
                        }
                        else
                        {
                            WriteToLog(String.Format("ReadArchive: не совпадает CRC {0} в ответе {1}",
                                BitConverter.ToString(crc16), BitConverter.ToString(in_buffer)));
                        }
                    } else
                    {
                       WriteToLog(String.Format("ReadArchive: не найдена команда {0} в ответе {1}",
                           BitConverter.ToString(m_cmd), BitConverter.ToString(in_buffer)));
                    }
                } else
                {
                    WriteToLog(String.Format("ReadArchive: WriteReadData на команду {0} вернул {1}",
                        BitConverter.ToString(m_cmd), BitConverter.ToString(in_buffer)));
                }
            } 

            return true;
        }

        #region Вспомогательные функции

        /// <summary>
        /// CRC16 
        /// </summary>
        /// <param name="Arr"></param>
        /// <returns></returns>
        private byte[] CRC16(byte[] Arr, int length)
        {
            byte[] CRC = new byte[2];
            UInt16 B = 0xFFFF;
            int j = 0;
            int i;
            byte b;
            bool f;

            unchecked
            {
                do
                {
                    i = 0;
                    b = Arr[j];
                    B = (UInt16)(B ^ (UInt16)b);
                    do
                    {
                        f = (((B) & (1)) == 1);
                        B = (UInt16)(B / 2);
                        if (f)
                        {
                            B = (UInt16)(B ^ (0xA001));
                        }
                        i++;
                    } while (i < 8);
                    j++;
                } while (j < length);
                CRC[0] = (byte)(B);
                CRC[1] = (byte)(B >> 8);
            }
            return CRC;
        }

        /// <summary>
        /// Конвертация Int в BCD
        /// </summary>
        /// <param name="val"></param>
        /// <param name="buf"></param>
        /// <returns></returns>
        private int Int2BCD(int val, byte[] buf)
        {
            int idx = buf.Length;
            unchecked
            {
                do
                {
                    idx--;
                    buf[idx] = (byte)((val % 10) | (((val % 100) / 10) << 4));
                    val /= 100;
                } while (val != 0);

                while (idx > 0)
                {
                    idx--;
                    buf[idx] = 0;
                }
            }

            return idx;
        }

        #endregion


        #region Реализация методов интерфейса

        public void Init(uint address, string pass, VirtualPort data_vport)
        {
            m_address = address;
            this.m_vport = data_vport;


            for (byte t = 1; t <= m_max_canals; t++)
            {
                m_dictDataTypes.Add(t, "");
            }
            //m_log_file_name += this.GetType() + "_" + m_address.ToString();
        }

        private int FindPacketSignature(Queue<byte> queue)
        {
            try
            {
                byte[] array = new byte[queue.Count];
                array = queue.ToArray();

                // зачем то, в СО массив записывается в очередь от последнего элемента к 0му
                // из за этого, неправильно определяется размер сообщения L и функция проверки ложно
                // срабатывает
                Array.Reverse(array);

                bool resCheck = CheckReceivedBytes(array, "FindPacketSignature");
                WriteToLog("FindPacketSignature, resCheck: " + resCheck);
                if (resCheck == true)
                    return 1;
                else
                    return -1;
            }
            catch
            {
                return -1;
            }
        }

        public List<byte> GetTypesForCategory(CommonCategory common_category)
        {
            List<byte> listTypes = new List<byte>();

            switch (common_category)
            {
                case CommonCategory.Current:
                case CommonCategory.Monthly:
                case CommonCategory.Daily:
                    for (byte type = 1; type <= m_max_canals; type++)
                    {
                        listTypes.Add(type);
                    }
                    break;
                case CommonCategory.Inday:
                    break;
            }

            return listTypes;
        }

        public bool OpenLinkCanal()
        {
            string serialTmp = "";
            return ReadSerialNumber(ref serialTmp);
        }

        public bool ReadSerialNumber(ref string serial_number)
        {
            try
            {
                m_length_cmd = 0;
                Byte[] in_buffer = new Byte[255];

                byte func = 0x0A;
                byte out_packet_length = 0x0C;
                m_cmd = new byte[out_packet_length];

                ushort rnd = 0x50b9;

                // адрес
                byte[] adr = new byte[4];
                Int2BCD((int)m_address, adr);

                byte[] parameter = { 0x02, 0x0 };
                byte[] random = BitConverter.GetBytes(rnd);

                // формируем команду 
                // адрес
                for (int i = 0; i < adr.Length; i++)
                    m_cmd[m_length_cmd++] = adr[i];

                // номер функции
                m_cmd[m_length_cmd++] = func;
                // общая длина пакета
                m_cmd[m_length_cmd++] = out_packet_length;


                // параметры
                for (int i = 0; i < parameter.Length; i++)
                    m_cmd[m_length_cmd++] = parameter[i];

                // ID
                for (int i = 0; i < random.Length; i++)
                    m_cmd[m_length_cmd++] = random[i];

                // CRC16
                byte[] crc16 = CRC16(m_cmd, m_length_cmd);
                for (int i = 0; i < crc16.Length; i++)
                    m_cmd[m_length_cmd++] = crc16[i];


                // WriteToLog("ReadCurrentValues: Исходящие: " + BitConverter.ToString(m_cmd));
                if (m_vport.WriteReadData(FindPacketSignature, m_cmd, ref in_buffer, m_length_cmd, -1) > 0)
                {
                    //WriteToLog("ReadCurrentValues: Входящие: " + BitConverter.ToString(in_buffer));
                    //WriteToLog("WriteReadData");
                    bool find_header = true;

                    if (!CheckReceivedBytes(in_buffer, "ReadSerialNumber"))
                        return false;

                    // длина пакета 
                    byte packet_length = 0;

                    // проверка заголовка пакета
                    for (int i = 0; i < 5; i++)
                    {
                        if (m_cmd[i] != in_buffer[i])
                        {
                            find_header = false;
                        }
                    }

                    if (find_header)
                    {
                        //WriteToLog("find_header");
                        packet_length = in_buffer[5];

                        // проверка CRC
                        crc16 = CRC16(in_buffer, packet_length - 2);

                        if (in_buffer[packet_length - 2] == crc16[0] && in_buffer[packet_length - 1] == crc16[1])
                        {
                            // проверка ID
                            if (m_cmd[out_packet_length - 4] == in_buffer[packet_length - 4] && m_cmd[out_packet_length - 3] == in_buffer[packet_length - 3])
                            {
                                byte[] meterAddrArr = new byte[8];
                                int stInd = 6;
                                int l = 8;
                                serial_number = "";

                                try
                                {
                                    for (int i = stInd + l - 1; i >= stInd; i--)
                                    {
                                        if (serial_number == "" && in_buffer[i] == 0x0)
                                            continue;

                                        serial_number += BitConverter.ToString(in_buffer, i, 1);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    WriteToLog("ReadSerialNumber: " + ex);
                                    return false;
                                }

                                //WriteToLog("Серийник: " + serial_number);
                                return true;
                            }
                            else
                            {
                                WriteToLog("ReadSerialNumber: неверный id");
                            }
                        }
                        else
                        {
                            WriteToLog("ReadSerialNumber: неверный CRC");
                        }
                    }
                    else
                    {
                        WriteToLog("ReadSerialNumber: первые 5 байт не равняются отправленной команде");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteToLog("ReadSerialNumber: " + ex);
            }

            return false;
        }

        public bool ReadSoftwareVersion(ref string softwareVersion)
        {

            try
            {

                softwareVersion = "";
                m_length_cmd = 0;
                Byte[] in_buffer = new Byte[255];

                byte func = 0x0A;
                byte out_packet_length = 0x0C;
                m_cmd = new byte[out_packet_length];

                ushort rnd = 0x50b9;

                // адрес
                byte[] adr = new byte[4];
                Int2BCD((int)m_address, adr);

                byte[] parameter = { 0x05, 0x0 };
                byte[] random = BitConverter.GetBytes(rnd);

                // формируем команду 
                // адрес
                for (int i = 0; i < adr.Length; i++)
                    m_cmd[m_length_cmd++] = adr[i];

                // номер функции
                m_cmd[m_length_cmd++] = func;
                // общая длина пакета
                m_cmd[m_length_cmd++] = out_packet_length;


                // параметры
                for (int i = 0; i < parameter.Length; i++)
                    m_cmd[m_length_cmd++] = parameter[i];

                // ID
                for (int i = 0; i < random.Length; i++)
                    m_cmd[m_length_cmd++] = random[i];

                // CRC16
                byte[] crc16 = CRC16(m_cmd, m_length_cmd);
                for (int i = 0; i < crc16.Length; i++)
                    m_cmd[m_length_cmd++] = crc16[i];


                // WriteToLog("ReadCurrentValues: Исходящие: " + BitConverter.ToString(m_cmd));
                if (m_vport.WriteReadData(FindPacketSignature, m_cmd, ref in_buffer, m_length_cmd, -1) > 0)
                {
                    //WriteToLog("ReadCurrentValues: Входящие: " + BitConverter.ToString(in_buffer));
                    //WriteToLog("WriteReadData");
                    bool find_header = true;

                    // длина пакета 
                    byte packet_length = 0;

                    if (!CheckReceivedBytes(in_buffer, "ReadSoftwareVersion"))
                        return false;

                    // проверка заголовка пакета
                    for (int i = 0; i < 5; i++)
                    {
                        if (m_cmd[i] != in_buffer[i])
                        {
                            find_header = false;
                        }
                    }

                    if (find_header)
                    {
                        //WriteToLog("find_header");
                        packet_length = in_buffer[5];

                        // проверка CRC
                        crc16 = CRC16(in_buffer, packet_length - 2);

                        if (in_buffer[packet_length - 2] == crc16[0] && in_buffer[packet_length - 1] == crc16[1])
                        {
                            // проверка ID
                            if (m_cmd[out_packet_length - 4] == in_buffer[packet_length - 4] && m_cmd[out_packet_length - 3] == in_buffer[packet_length - 3])
                            {
                                int stInd = 6;
                                try
                                {
                                    uint swVersion = BitConverter.ToUInt16(in_buffer, stInd);
                                    softwareVersion = swVersion.ToString();
                                }
                                catch (Exception ex)
                                {
                                    WriteToLog("ReadSoftwareVersion: " + ex);
                                    return false;
                                }

                                //WriteToLog("Серийник: " + serial_number);
                                return true;
                            }
                            else
                            {
                                WriteToLog("ReadSoftwareVersion: неверный id");
                            }
                        }
                        else
                        {
                            WriteToLog("ReadSoftwareVersion: неверный CRC");
                        }
                    }
                    else
                    {
                        WriteToLog("ReadSoftwareVersion: первые 5 байт не равняются отправленной команде");
                    }
                }
            }
            catch(Exception ex)
            {
                WriteToLog("ReadSoftwareVersion: " + ex);
            }

                return false;
            }

        public bool ReadTimeOn(ref string timeOn)
        {
            timeOn = "";
            m_length_cmd = 0;
            Byte[] in_buffer = new Byte[255];

            byte func = 0x0A;
            byte out_packet_length = 0x0C;
            m_cmd = new byte[out_packet_length];

            ushort rnd = 0x50b9;

            // адрес
            byte[] adr = new byte[4];
            Int2BCD((int)m_address, adr);

            byte[] parameter = { 0x0C, 0x0};
            byte[] random = BitConverter.GetBytes(rnd);

            // формируем команду 
            // адрес
            for (int i = 0; i < adr.Length; i++)
                m_cmd[m_length_cmd++] = adr[i];

            // номер функции
            m_cmd[m_length_cmd++] = func;
            // общая длина пакета
            m_cmd[m_length_cmd++] = out_packet_length;


            // параметры
            for (int i = 0; i < parameter.Length; i++)
                m_cmd[m_length_cmd++] = parameter[i];

            // ID
            for (int i = 0; i < random.Length; i++)
                m_cmd[m_length_cmd++] = random[i];

            // CRC16
            byte[] crc16 = CRC16(m_cmd, m_length_cmd);
            for (int i = 0; i < crc16.Length; i++)
                m_cmd[m_length_cmd++] = crc16[i];


            // WriteToLog("ReadCurrentValues: Исходящие: " + BitConverter.ToString(m_cmd));
            if (m_vport.WriteReadData(FindPacketSignature, m_cmd, ref in_buffer, m_length_cmd, -1) > 0)
            {
                //WriteToLog("ReadCurrentValues: Входящие: " + BitConverter.ToString(in_buffer));
                //WriteToLog("WriteReadData");
                bool find_header = true;

                // длина пакета 
                byte packet_length = 0;

                if (!CheckReceivedBytes(in_buffer, "ReadTimeOn"))
                    return false;

                // проверка заголовка пакета
                for (int i = 0; i < 5; i++)
                {
                    if (m_cmd[i] != in_buffer[i])
                    {
                        find_header = false;
                    }
                }

                if (find_header)
                {
                    //WriteToLog("find_header");
                    packet_length = in_buffer[5];

                    // проверка CRC
                    crc16 = CRC16(in_buffer, packet_length - 2);

                    if (in_buffer[packet_length - 2] == crc16[0] && in_buffer[packet_length - 1] == crc16[1])
                    {
                        // проверка ID
                        if (m_cmd[out_packet_length - 4] == in_buffer[packet_length - 4] && m_cmd[out_packet_length - 3] == in_buffer[packet_length - 3])
                        {
                            int stInd = 6;
                            try
                            {
                                uint swVersion = BitConverter.ToUInt32(in_buffer, stInd);
                                timeOn = swVersion.ToString();
                            }
                            catch (Exception ex)
                            {
                                WriteToLog("ReadSoftwareVersion: " + ex);
                                return false;
                            }

                            //WriteToLog("Серийник: " + serial_number);
                            return true;
                        }
                        else
                        {
                            WriteToLog("ReadSoftwareVersion: неверный id");
                        }
                    }
                    else
                    {
                        WriteToLog("ReadSoftwareVersion: неверный CRC");
                    }
                }
                else
                {
                    WriteToLog("ReadSoftwareVersion: первые 5 байт не равняются отправленной команде");
                }
            }

            return false;
        }

        public bool ReadTimeOnErr(ref string timeOnErr)
        {
            timeOnErr = "";
            m_length_cmd = 0;
            Byte[] in_buffer = new Byte[255];

            byte func = 0x0A;
            byte out_packet_length = 0x0C;
            m_cmd = new byte[out_packet_length];

            ushort rnd = 0x50b9;

            // адрес
            byte[] adr = new byte[4];
            Int2BCD((int)m_address, adr);

            byte[] parameter = { 0x0D, 0x0 };
            byte[] random = BitConverter.GetBytes(rnd);

            // формируем команду 
            // адрес
            for (int i = 0; i < adr.Length; i++)
                m_cmd[m_length_cmd++] = adr[i];

            // номер функции
            m_cmd[m_length_cmd++] = func;
            // общая длина пакета
            m_cmd[m_length_cmd++] = out_packet_length;


            // параметры
            for (int i = 0; i < parameter.Length; i++)
                m_cmd[m_length_cmd++] = parameter[i];

            // ID
            for (int i = 0; i < random.Length; i++)
                m_cmd[m_length_cmd++] = random[i];

            // CRC16
            byte[] crc16 = CRC16(m_cmd, m_length_cmd);
            for (int i = 0; i < crc16.Length; i++)
                m_cmd[m_length_cmd++] = crc16[i];


            // WriteToLog("ReadCurrentValues: Исходящие: " + BitConverter.ToString(m_cmd));
            if (m_vport.WriteReadData(FindPacketSignature, m_cmd, ref in_buffer, m_length_cmd, -1) > 0)
            {
                //WriteToLog("ReadCurrentValues: Входящие: " + BitConverter.ToString(in_buffer));
                //WriteToLog("WriteReadData");
                bool find_header = true;

                // длина пакета 
                byte packet_length = 0;

                if (!CheckReceivedBytes(in_buffer, "ReadTimeOnErr"))
                    return false;

                // проверка заголовка пакета
                for (int i = 0; i < 5; i++)
                {
                    if (m_cmd[i] != in_buffer[i])
                    {
                        find_header = false;
                    }
                }

                if (find_header)
                {
                    //WriteToLog("find_header");
                    packet_length = in_buffer[5];

                    // проверка CRC
                    crc16 = CRC16(in_buffer, packet_length - 2);

                    if (in_buffer[packet_length - 2] == crc16[0] && in_buffer[packet_length - 1] == crc16[1])
                    {
                        // проверка ID
                        if (m_cmd[out_packet_length - 4] == in_buffer[packet_length - 4] && m_cmd[out_packet_length - 3] == in_buffer[packet_length - 3])
                        {
                            int stInd = 6;
                            try
                            {
                                uint swVersion = BitConverter.ToUInt32(in_buffer, stInd);
                                timeOnErr = swVersion.ToString();
                            }
                            catch (Exception ex)
                            {
                                WriteToLog("ReadSoftwareVersion: " + ex);
                                return false;
                            }

                            //WriteToLog("Серийник: " + serial_number);
                            return true;
                        }
                        else
                        {
                            WriteToLog("ReadSoftwareVersion: неверный id");
                        }
                    }
                    else
                    {
                        WriteToLog("ReadSoftwareVersion: неверный CRC");
                    }
                }
                else
                {
                    WriteToLog("ReadSoftwareVersion: первые 5 байт не равняются отправленной команде");
                }
            }

            return false;
        }

        public bool ReadMeterType(ref string softwareVersion)
        {
            bool res = this.readMeterType(ref softwareVersion);
            if (!res)
                res = this.readMeterType(ref softwareVersion, true);

            return res;
        }

        private bool readMeterType(ref string softwareVersion, bool alt = false)
        {
            try
            {
                softwareVersion = "";
                m_length_cmd = 0;
                Byte[] in_buffer = new Byte[255];

                m_cmd = new byte[11];

                if (alt)
                    m_cmd = new byte[12];

                // адрес
                byte[] adr = new byte[4];
                Int2BCD((int)m_address, adr);


                byte[] parameter = { 0x03, 0x02, 0x46, 0x0, 0x01 };
                byte[] parameterAlternative = { 0x0A, 0x0C, 0x0, 0x0, 0xF0, 0x1F };

                if (alt)
                    parameter = parameterAlternative;



                // формируем команду 
                // адрес
                for (int i = 0; i < adr.Length; i++)
                    m_cmd[m_length_cmd++] = adr[i];

                // параметры
                for (int i = 0; i < parameter.Length; i++)
                    m_cmd[m_length_cmd++] = parameter[i];

                // CRC16
                byte[] crc16 = CRC16(m_cmd, m_length_cmd);
                for (int i = 0; i < crc16.Length; i++)
                    m_cmd[m_length_cmd++] = crc16[i];


                WriteToLog("ReadMeterType: Исходящие: " + BitConverter.ToString(m_cmd));
                if (m_vport.WriteReadData(FindPacketSignature, m_cmd, ref in_buffer, m_length_cmd, -1) > 0)
                {
                    WriteToLog("ReadMeterType: Входящие: " + BitConverter.ToString(in_buffer));
                    bool find_header = true;

                    // длина пакета 
                    byte packet_length = (byte)in_buffer.Length;

                    if (!CheckReceivedBytes(in_buffer, "ReadMeterType"))
                        return false;

                    // проверка заголовка пакета
                    for (int i = 0; i < 5; i++)
                    {
                        if (m_cmd[i] != in_buffer[i])
                        {
                            find_header = false;
                        }
                    }

                    if (find_header)
                    {
                        // проверка CRC
                        crc16 = CRC16(in_buffer, packet_length - 2);

                        if (in_buffer[packet_length - 2] == crc16[0] && in_buffer[packet_length - 1] == crc16[1])
                        {
                            int stInd = 6;
                            try
                            {
                                uint swVersion = BitConverter.ToUInt16(in_buffer, stInd);
                                if (Enum.IsDefined(typeof(PulsarMeterTypes), (int)swVersion))
                                {
                                    this.meterType = (PulsarMeterTypes)(int)swVersion;
                                    softwareVersion = Enum.GetName(typeof(PulsarMeterTypes), (int)swVersion);
                                    return true;
                                }
                                else
                                {
                                    softwareVersion = swVersion.ToString();
                                    WriteToLog("ReadMeterType: модели счетчика типа " + swVersion + " нет в перечислении PulsarMeterTypes");
                                    return false;
                                }
                            }
                            catch (Exception ex)
                            {
                                WriteToLog("ReadMeterType: " + ex);
                                return false;
                            }

                            //WriteToLog("Серийник: " + serial_number);

                        }
                        else
                        {
                            WriteToLog("ReadMeterType: неверный CRC");
                        }
                    }
                    else
                    {
                        WriteToLog("ReadMeterType: первые 5 байт не равняются отправленной команде");
                    }
                }
                else
                {
                    WriteToLog("ReadMeterType: WriteReadData вернул 0 байт на запрос по параметрам  0x03, 0x02, 0x46, 0x0, 0x01.");
                }
            }
            catch (Exception ex)
            {
                WriteToLog("ReadMeterType: " + ex);
            }

            return false;
        }

        public bool SyncTime(DateTime dt)
        {
            m_length_cmd = 0;
            byte out_packet_length = 16;
            int bytes_to_read = 14;
            Byte[] in_buffer = new Byte[255];

            m_cmd = new byte[out_packet_length];

            // формируем команду 
            // адрес
            byte[] adr = new byte[4];
            Int2BCD((int)m_address, adr);
            for (int i = 0; i < adr.Length; i++)
            {
                m_cmd[m_length_cmd++] = adr[i];
            }
            // номер функции
            m_cmd[m_length_cmd++] = 5;
            // общая длина пакета
            m_cmd[m_length_cmd++] = out_packet_length;
            // параметры /*Время*/
            m_cmd[m_length_cmd++] = Convert.ToByte(dt.Year - 2000);
            m_cmd[m_length_cmd++] = Convert.ToByte(dt.Month);
            m_cmd[m_length_cmd++] = Convert.ToByte(dt.Day);
            m_cmd[m_length_cmd++] = Convert.ToByte(dt.Hour);
            m_cmd[m_length_cmd++] = Convert.ToByte(dt.Minute);
            m_cmd[m_length_cmd++] = Convert.ToByte(dt.Second);
            // ID
            ushort rnd = 0x50b9;
            byte[] random = BitConverter.GetBytes(rnd);
            for (int i = 0; i < random.Length; i++)
            {
                m_cmd[m_length_cmd++] = random[i];
            }
            // CRC16
            byte[] crc16 = CRC16(m_cmd, m_length_cmd);
            for (int i = 0; i < crc16.Length; i++)
            {
                m_cmd[m_length_cmd++] = crc16[i];
            }

            if (m_vport != null)
            {
                if (m_vport.WriteReadData(FindPacketSignature, m_cmd, ref in_buffer, m_length_cmd, -1) > 0)
                {
                    bool find_header = true;

                    // длина пакета 
                    byte packet_length = 0;

                    // проверка заголовка пакета
                    for (int i = 0; i < 5; i++)
                    {
                        if (m_cmd[i] != in_buffer[i])
                        {
                            find_header = false;
                        }
                    }

                    if (find_header)
                    {
                        packet_length = in_buffer[5];
                        // проверка CRC
                        crc16 = CRC16(in_buffer, packet_length - 2);

                        if (in_buffer[packet_length - 2] == crc16[0] && in_buffer[packet_length - 1] == crc16[1])
                        {
                            // проверка ID
                            if (m_cmd[out_packet_length - 4] == in_buffer[packet_length - 4] && m_cmd[out_packet_length - 3] == in_buffer[packet_length - 3])
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public bool ReadTime(ref DateTime meterDate)
        {
            m_length_cmd = 0;
            Byte[] in_buffer = new Byte[255];

            byte func = 0x04;
            byte out_packet_length = 0x0A;
            m_cmd = new byte[out_packet_length];

            ushort rnd = 0x50b9;

            // адрес
            byte[] adr = new byte[4];
            Int2BCD((int)m_address, adr);

            byte[] random = BitConverter.GetBytes(rnd);

            // формируем команду 
            // адрес
            for (int i = 0; i < adr.Length; i++)
                m_cmd[m_length_cmd++] = adr[i];

            // номер функции
            m_cmd[m_length_cmd++] = func;
            // общая длина пакета
            m_cmd[m_length_cmd++] = out_packet_length;

            // ID
            for (int i = 0; i < random.Length; i++)
                m_cmd[m_length_cmd++] = random[i];

            // CRC16
            byte[] crc16 = CRC16(m_cmd, m_length_cmd);
            for (int i = 0; i < crc16.Length; i++)
                m_cmd[m_length_cmd++] = crc16[i];


            // WriteToLog("ReadCurrentValues: Исходящие: " + BitConverter.ToString(m_cmd));
            if (m_vport.WriteReadData(FindPacketSignature, m_cmd, ref in_buffer, m_length_cmd, -1) > 0)
            {
                //WriteToLog("ReadCurrentValues: Входящие: " + BitConverter.ToString(in_buffer));
                //WriteToLog("WriteReadData");
                bool find_header = true;

                if (!CheckReceivedBytes(in_buffer, "ReadTime"))
                    return false;

                // длина пакета 
                byte packet_length = 0;

                // проверка заголовка пакета
                for (int i = 0; i < 5; i++)
                {
                    if (m_cmd[i] != in_buffer[i])
                    {
                        find_header = false;
                    }
                }

                if (find_header)
                {
                    //WriteToLog("find_header");
                    packet_length = in_buffer[5];

                    // проверка CRC
                    crc16 = CRC16(in_buffer, packet_length - 2);

                    if (in_buffer[packet_length - 2] == crc16[0] && in_buffer[packet_length - 1] == crc16[1])
                    {
                        // проверка ID
                        if (m_cmd[out_packet_length - 4] == in_buffer[packet_length - 4] && m_cmd[out_packet_length - 3] == in_buffer[packet_length - 3])
                        {

                            // после всех проверок можно разбирать дату

                            int stInd = 6;

                            try
                            {
                                DateTime dt = new DateTime(in_buffer[stInd] + 2000, in_buffer[stInd + 1],
                                    in_buffer[stInd + 2], in_buffer[stInd + 3], in_buffer[stInd + 4], in_buffer[stInd + 5]);
                                meterDate = new DateTime(dt.Ticks);

                                return true;
                            }
                            catch (Exception ex)
                            {
                                WriteToLog("Meter date parse exception: " + ex);
                                WriteToLog(BitConverter.ToString(in_buffer));
                                return false;
                            }
                        }
                        else
                        {
                            WriteToLog("ReadSerialNumber: неверный id");
                        }
                    }
                    else
                    {
                        WriteToLog("ReadSerialNumber: неверный CRC");
                    }
                }
                else
                {
                    WriteToLog("ReadSerialNumber: первые 5 байт не равняются отправленной команде");
                }
            }
  
            return false;
        }

        public bool ReadCurrentValues(ushort param, ushort tarif, ref float recordValue)
        {

            List<byte> types = new List<byte>();
            types.Add((byte)(param & 0x000000FF));
            ClearTypesForRead();
            SetTypesForRead(types);
            Values vals = new Values();
            if (ReadCurrentValues(ref vals))
            {
                if (vals.listRV.Count == 1)
                {
                    RecordValue rv = vals.listRV[vals.listRV.Count - 1];
                    recordValue = (float)rv.value;
                    return rv.fine_state;
                }
            }
            WriteToLog("ReadCurrentValues: невозможно считать текущее значение для параметра " + param.ToString());
            return false;

            /*
            SetTypesForRead(GetTypesForCategory(CommonCategory.Current));
            Values vals = new Values();
            vals.listRV = new List<RecordValue>();
            if (ReadCurrentValues(ref vals))
            {

                return true;
            }

            return false;*/
        }

        public bool ReadMonthlyValues(DateTime dt, ushort param, ushort tarif, ref float recordValue)
        {
            List<byte> types = new List<byte>();
            types.Add((byte)(param & 0x000000FF));
            ClearTypesForRead();
            SetTypesForRead(types);
            Values vals = new Values();
            if (ReadMonthlyValues((byte)dt.Month, (ushort)dt.Year, ref vals))
            {
                if (vals.listRV.Count == 1)
                {
                    RecordValue rv = vals.listRV[vals.listRV.Count - 1];
                    recordValue = (float)rv.value;
                    return rv.fine_state;
                }
            }

            WriteToLog("ReadMonthlyValues: невозможно считать текущее значение для параметра " + param.ToString());
            return false;
        }

        public bool ReadDailyValues(DateTime dt, ushort param, ushort tarif, ref float recordValue)
        {
            List<byte> types = new List<byte>();
            types.Add((byte)(param & 0x000000FF));
            ClearTypesForRead();
            SetTypesForRead(types);
            Values vals = new Values();
            if (ReadDailyValues((byte)dt.Day, (byte)dt.Month, (ushort)dt.Year, ref vals))
            {
                if (vals.listRV.Count == 1)
                {
                    RecordValue rv = vals.listRV[vals.listRV.Count - 1];
                    recordValue = (float)rv.value;
                    return rv.fine_state;
                }
            }

            WriteToLog("ReadDailyValues: невозможно считать текущее значение для параметра " + param.ToString());
            return false;
        }

        #endregion

        #region Неиспользуемые методы интерфейса
        public bool ReadPowerSlice(ref List<SliceDescriptor> sliceUniversalList, DateTime dt_end, SlicePeriod period)
        {
            return false;
        }
        public bool ReadPowerSlice(DateTime dt_begin, DateTime dt_end, ref List<RecordPowerSlice> listRPS, byte period)
        {
            return false;
        }
        public bool ReadSliceArrInitializationDate(ref DateTime lastInitDt)
        {
            return false;
        }

        public List<byte> GetTypesForCategory(LibMeter.CommonCategory common_category)
        {
            List<byte> tmp = new List<byte>();
            return tmp;
        }

        public bool ReadDailyValues(uint recordId, ushort param, ushort tarif, ref float recordValue)
        {
            return false;
        }


        #endregion
    }

    public enum PulsarMeterTypes
    {
        kompaktniy_teplo_v3 = 0x010F,
        kompaktniy_teplo_v4 = 0x0121,
        voda_v6 = 0x0129,
        voda_rs485 = 0x62,
        pulsarM = 0x0104,
        voda_v11 = 0x013F
    }

    #region Заимствованные структуры

    /// <summary>
    /// Общие категории считываемых данных
    /// </summary>
    public enum CommonCategory
    {
        /// <summary>
        /// Текущие значения
        /// </summary>
        Current = 1,
        /// <summary>
        /// Значения на начало месяца
        /// </summary>
        Monthly = 2,
        /// <summary>
        /// Внутридневные значения
        /// </summary>
        Inday = 3,
        /// <summary>
        /// Значения на начало суток
        /// </summary>
        Daily = 4
    };


    public enum TypeDataPulsar : byte
    {
        Current = 0,
        Hourly = 1,
        Daily = 2,
        Monthly = 3
    }

    /// <summary>
    /// Структура с иформацией об срезе мощности
    /// </summary>
    public struct IndaySlice
    {
        /// <summary>
        /// Коллекция со значениями
        /// </summary>
        public List<RecordValue> values;
        /// <summary>
        /// Статус значений
        /// </summary>
        public bool not_full;
        /// <summary>
        /// Время среза
        /// </summary>
        public DateTime date_time;
    };

    /// <summary>
    ///  Структура с информацией о считанных величинах
    /// </summary>
    public struct Values
    {
        /// <summary>
        /// Коллекция с информацией о считанных величинах
        /// </summary>
        public List<RecordValue> listRV;
    }

    /// <summary>
    /// Структура с информацией об единичной считываемой величине  
    /// </summary>
    public struct RecordValue
    {
        /// <summary>
        /// Значение
        /// </summary>
        public double value;
        /// <summary>
        /// Тип
        /// </summary>
        public byte type;
        /// <summary>
        /// Статус (true - значение верно, false - неверно)
        /// </summary>
        public bool fine_state;
    };

    #endregion


}
