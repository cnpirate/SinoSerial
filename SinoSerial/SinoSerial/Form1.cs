using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.IO.Ports;
using System.Threading;

namespace SinoSerial
{
	public partial class SinoSerial : Form
	{
		static bool _continue;
		static SerialPort _serialPort;
		static Thread readThread;    
		static byte[] cmd = new byte[4];
		static byte[] ack = new byte[4];
		static CmdData cmdData;
		static CmdData ackData;
		static byte segNo = 0;
		static byte segTotal = 0;
		static int byteNo = 0;
		static FileStream s_binFs;
		static BinaryReader s_binReader;
		static bool SendNeeded = false;

		static UInt16[] Crc16Table = {
		0x0000, 0x1021, 0x2042, 0x3063, 0x4084, 0x50a5, 0x60c6, 0x70e7,
		0x8108, 0x9129, 0xa14a, 0xb16b, 0xc18c, 0xd1ad, 0xe1ce, 0xf1ef,
		0x1231, 0x0210, 0x3273, 0x2252, 0x52b5, 0x4294, 0x72f7, 0x62d6,
		0x9339, 0x8318, 0xb37b, 0xa35a, 0xd3bd, 0xc39c, 0xf3ff, 0xe3de,
		0x2462, 0x3443, 0x0420, 0x1401, 0x64e6, 0x74c7, 0x44a4, 0x5485,
		0xa56a, 0xb54b, 0x8528, 0x9509, 0xe5ee, 0xf5cf, 0xc5ac, 0xd58d,
		0x3653, 0x2672, 0x1611, 0x0630, 0x76d7, 0x66f6, 0x5695, 0x46b4,
		0xb75b, 0xa77a, 0x9719, 0x8738, 0xf7df, 0xe7fe, 0xd79d, 0xc7bc,
		0x48c4, 0x58e5, 0x6886, 0x78a7, 0x0840, 0x1861, 0x2802, 0x3823,
		0xc9cc, 0xd9ed, 0xe98e, 0xf9af, 0x8948, 0x9969, 0xa90a, 0xb92b,
		0x5af5, 0x4ad4, 0x7ab7, 0x6a96, 0x1a71, 0x0a50, 0x3a33, 0x2a12,
		0xdbfd, 0xcbdc, 0xfbbf, 0xeb9e, 0x9b79, 0x8b58, 0xbb3b, 0xab1a,
		0x6ca6, 0x7c87, 0x4ce4, 0x5cc5, 0x2c22, 0x3c03, 0x0c60, 0x1c41,
		0xedae, 0xfd8f, 0xcdec, 0xddcd, 0xad2a, 0xbd0b, 0x8d68, 0x9d49,
		0x7e97, 0x6eb6, 0x5ed5, 0x4ef4, 0x3e13, 0x2e32, 0x1e51, 0x0e70,
		0xff9f, 0xefbe, 0xdfdd, 0xcffc, 0xbf1b, 0xaf3a, 0x9f59, 0x8f78,
		0x9188, 0x81a9, 0xb1ca, 0xa1eb, 0xd10c, 0xc12d, 0xf14e, 0xe16f,
		0x1080, 0x00a1, 0x30c2, 0x20e3, 0x5004, 0x4025, 0x7046, 0x6067,
		0x83b9, 0x9398, 0xa3fb, 0xb3da, 0xc33d, 0xd31c, 0xe37f, 0xf35e,
		0x02b1, 0x1290, 0x22f3, 0x32d2, 0x4235, 0x5214, 0x6277, 0x7256,
		0xb5ea, 0xa5cb, 0x95a8, 0x8589, 0xf56e, 0xe54f, 0xd52c, 0xc50d,
		0x34e2, 0x24c3, 0x14a0, 0x0481, 0x7466, 0x6447, 0x5424, 0x4405,
		0xa7db, 0xb7fa, 0x8799, 0x97b8, 0xe75f, 0xf77e, 0xc71d, 0xd73c,
		0x26d3, 0x36f2, 0x0691, 0x16b0, 0x6657, 0x7676, 0x4615, 0x5634,
		0xd94c, 0xc96d, 0xf90e, 0xe92f, 0x99c8, 0x89e9, 0xb98a, 0xa9ab,
		0x5844, 0x4865, 0x7806, 0x6827, 0x18c0, 0x08e1, 0x3882, 0x28a3,
		0xcb7d, 0xdb5c, 0xeb3f, 0xfb1e, 0x8bf9, 0x9bd8, 0xabbb, 0xbb9a,
		0x4a75, 0x5a54, 0x6a37, 0x7a16, 0x0af1, 0x1ad0, 0x2ab3, 0x3a92,
		0xfd2e, 0xed0f, 0xdd6c, 0xcd4d, 0xbdaa, 0xad8b, 0x9de8, 0x8dc9,
		0x7c26, 0x6c07, 0x5c64, 0x4c45, 0x3ca2, 0x2c83, 0x1ce0, 0x0cc1,
		0xef1f, 0xff3e, 0xcf5d, 0xdf7c, 0xaf9b, 0xbfba, 0x8fd9, 0x9ff8,
		0x6e17, 0x7e36, 0x4e55, 0x5e74, 0x2e93, 0x3eb2, 0x0ed1, 0x1ef0
		};

		private UInt16 get_crc_16(byte[] data, int count)
		{
			UInt16 crc = 0;

			return get_crc_16_ex(crc, data, count);
		}

		private UInt16 get_crc_16_ex(UInt16 crc, byte[] data, int count)
		{
			int i = 0;
			crc ^= 0xFFFF;

			while ((count--)!=0)
			{
				crc = (UInt16)((Crc16Table[(crc >> 8) ^ (data[i])] ^ ((crc << 8) & 0xFFFF)) & 0xFFFF);
				i++;
			}

			return (UInt16)(crc ^ 0xFFFF);
		}

		private UInt16 get_file_crc_16(string fileName)
		{
			long i = 0;
			byte[] data = new byte[1];
			UInt16 crc = 0;
			long fileLength = 0;
			FileStream binFs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
			BinaryReader binReader = new BinaryReader(binFs);

			fileLength = binFs.Length;
			if (fileLength % 1024 != 0)
			{
				fileLength = ((fileLength / 1024) + 1)*1024;
			}

			for (i = 0; i < fileLength; i++)
			{
				if (i >= binFs.Length)
				{
					data[0] = 0;
				}
				else
				{
					data[0] = binReader.ReadByte();
				}
				crc = get_crc_16_ex(crc, data, 1);
			}
			
			binFs.Close();
			binReader.Close();
			return crc;
		}

		public struct CmdData
		{
			public byte Header;
			public byte Cmd;
			public UInt16 Data;
		};

		public enum EnumOperation : byte
		{
			Bengin = 0x01,
			Finish = 0x08,

			Flash = 0x90,
			Segment = 0x91,

			LoadHighBits = 0x92,
			LoadLowBits = 0x93,

			CalCrc = 0x94,
			GetCrc = 0x95,
		};

		private int SerialSend(CmdData cmdTmp)
		{
			cmd[0] = cmdTmp.Header;
			cmd[1] = cmdTmp.Cmd;
			cmd[2] = (byte)(cmdTmp.Data >> 8);
			cmd[3] = (byte)(cmdTmp.Data);

			_serialPort.Write(cmd, 0, 4);

			return 4;
		}

		private CmdData SerialRecv()
		{
			CmdData cmdRet;         
			int ret = 0;
			int i = 0;
			cmdRet.Header = 0;
			cmdRet.Cmd = 0;
			cmdRet.Data = 0;

			ack[0] = 0;
			ack[1] = 0;
			ack[2] = 0;
			ack[3] = 0;

            try
            {
                do
                {
                    ret = _serialPort.Read(ack, i, 1);
                    i++;
                    if (i == 4)
                    {
                        break;
                    }

                } while (i < 4);

                cmdRet.Header = ack[0];
                cmdRet.Cmd = ack[1];
                cmdRet.Data = (UInt16)((ack[2] << 8) | ack[3]);
                _serialPort.DiscardInBuffer();
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show(this, e.Message);
                _continue = false;

                _serialPort.Close();

                btnClose.Enabled = false;
                if (false == btnOpen.Enabled)
                {
                    btnOpen.Enabled = true;
                    btnSendCmd.Enabled = false;
                    btnFileSend.Enabled = false;
                    btnFileBrowser.Enabled = false;
                }

                readThread.Abort();
            }
            catch (TimeoutException e)
            { }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message);
                _continue = false;

                _serialPort.Close();

                btnClose.Enabled = false;
                if (false == btnOpen.Enabled)
                {
                    btnOpen.Enabled = true;
                    btnSendCmd.Enabled = false;
                    btnFileSend.Enabled = false;
                    btnFileBrowser.Enabled = false;
                }

                readThread.Abort();
            }
                        			
			return cmdRet;
		}

		private void InitialDownload()
		{
			segNo = 0;

			cmdData.Header = 0xAA;
			cmdData.Cmd = (byte)EnumOperation.Flash;
			cmdData.Data = ((UInt16)EnumOperation.Bengin) << 8;
		}

		private void FinishDownload()
		{
			segNo = 0;

			cmdData.Header = 0xAA;
			cmdData.Cmd = (byte)EnumOperation.Flash;
			cmdData.Data = ((UInt16)EnumOperation.Finish) << 8;
		}

		private void SegDownloadStart()
		{
			cmdData.Header = 0xAA;
			cmdData.Cmd = (byte)EnumOperation.Segment;
			cmdData.Data = (UInt16)((((UInt16)EnumOperation.Bengin) << 8) | (UInt16)segNo);
		}

		private void SegDownloadHighBits()
		{
			
			cmdData.Header = 0xAA;
			cmdData.Cmd = (byte)EnumOperation.LoadHighBits;
			try
			{
				cmdData.Data = s_binReader.ReadUInt16();
			}
			catch (EndOfStreamException)
			{
				if ((s_binFs.Length % 1024) != 0)
				{
					cmdData.Data = 0;
				}
			}

		}

		private void SegDownloadFinish()
		{
			cmdData.Header = 0xAA;
			cmdData.Cmd = (byte)EnumOperation.Segment;
			cmdData.Data = (UInt16)((((UInt16)EnumOperation.Finish) << 8) | (UInt16)segNo);
		}

		private void GetBinCrc()
		{
			cmdData.Header = 0xAA;
			cmdData.Cmd = (byte)EnumOperation.GetCrc;
			cmdData.Data = (UInt16)((0xFF << 8) | (UInt16)segNo);
			
		}

		public void Read()
		{
			int i = 0;
			string strRecv = null;
			DateTime now;
			byte flag;
			string strSend = null;

			while (_continue)
			{
				try
				{
					//retRead = _serialPort.Read(ack, 0, 4);
					//if (retRead > 0)
					//{
					//    retRead = 0;
					//    strRecv = "";
					//    _serialPort.DiscardInBuffer();
					//    for (i = 0; i < 4; i++)
					//    {
					//        strRecv += ack[i].ToString("X2");
					//    }
					//    now = DateTime.Now;
					//    tbScreen.Text += "    [" + now.ToString("yyyy-MM-dd hh:mm:ss") + "]  " + "Recv:  " + "0x" + strRecv + "\r\n";
					//}

					ackData = SerialRecv();
					if (ackData.Header != 0)
					{
						switch (ackData.Cmd)
						{
							case (byte)EnumOperation.Flash:
								SendNeeded = true;
								flag = (byte)(ackData.Data >> 8);
								if ((byte)EnumOperation.Bengin == flag)
								{
									segNo = 0;
									byteNo = 0;
									SegDownloadStart();
								}
								if ((byte)EnumOperation.Finish == flag)
								{
									SendNeeded = false;
									s_binFs.Close();
									s_binReader.Close();
                                    MessageBox.Show(this, "Dowload Sucussfully ^_^");
									
									// No data is needed to send
								}
								break;

							case (byte)EnumOperation.Segment:
								flag = (byte)(ackData.Data >> 8);
								SendNeeded = true;
								if ((byte)EnumOperation.Bengin == flag)
								{
									SegDownloadHighBits();
									
								}
								if ((byte)EnumOperation.Finish == flag)
								{
									segNo++;
									if (segNo == segTotal)
									{
										GetBinCrc();
									}
									else
									{
										byteNo = 0;
										SegDownloadStart();
									}
									
								}
								break;

							case (byte)EnumOperation.LoadHighBits:
								SendNeeded = true;
								byteNo = byteNo + 2;
								if (1024 == byteNo)
								{
									SegDownloadFinish();
								}
								else
								{
									SegDownloadHighBits();
								}
								
								break;

							case (byte)EnumOperation.GetCrc:
								// check crc
								// if crc is correct, send finish command; or tell user!!
								SendNeeded = true;
								UInt16 fileCrc = get_file_crc_16(tbFilePath.Text);
								if (fileCrc == ackData.Data)
								{
                                    MessageBox.Show(this, "Crc Correct!!");
									FinishDownload();
								}
								else
								{
									SendNeeded = false;
									s_binFs.Close();
									s_binReader.Close();
									string mesg = string.Format("CRC Error!! buff:{0:X}; cal:{1:X}", ackData.Data, fileCrc);
                                    MessageBox.Show(this, mesg);
									
								}

								break;

							default:								
								SendNeeded = false;
								break;
						}

						strRecv = "";
						for (i = 0; i < 4; i++)
						{
							strRecv += ack[i].ToString("X2");
						}
						now = DateTime.Now;
						tbScreen.Text += "    [" + now.ToString("yyyy-MM-dd hh:mm:ss") + "]  " + "Recv:  " + "0x" + strRecv + "\r\n";

						if (SendNeeded)
						{
							SerialSend(cmdData);
							strSend = "";
							for (i = 0; i < 4; i++)
							{
								strSend += cmd[i].ToString("X2");
							}

							now = DateTime.Now;
							tbScreen.Text += "[" + now.ToString("yyyy-MM-dd hh:mm:ss") + "]  " + "Send:  " + "0x" + strSend + "\r\n";
						}
						
					}
					
					
				}
				catch (TimeoutException) { }
			}
		}

		public SinoSerial()
		{
			InitializeComponent();
		}

		private void btnOpen_Click(object sender, EventArgs e)
		{
			readThread = new Thread(Read);

			_serialPort = new SerialPort();

			_serialPort.PortName = cbPortName.Text;
			_serialPort.BaudRate = Convert.ToInt32(cbBaudRate.Text);
			_serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), cbParity.Text);
			_serialPort.DataBits = Convert.ToInt32(cbDataBits.Text);
			_serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbStopBits.Text);
			_serialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), "None");

			// Set the read/write timeouts
			_serialPort.ReadTimeout = 3000;
			_serialPort.WriteTimeout = 3000;
			try
			{
				_serialPort.Open();
				_serialPort.DiscardInBuffer();
				_continue = true;
				readThread.Start();

				btnOpen.Enabled = false;
				btnClose.Enabled = true;
				btnSendCmd.Enabled = true;
				btnFileSend.Enabled = true;
				btnFileBrowser.Enabled = true;

			}
			catch (Exception ex)
			{
                MessageBox.Show(this, ex.Message);
				
			}



		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			_continue = false;
			readThread.Join();
			_serialPort.Close();            

			btnClose.Enabled = false;
			if (false == btnOpen.Enabled)
			{
				btnOpen.Enabled = true;
				btnSendCmd.Enabled = false;
				btnFileSend.Enabled = false;
				btnFileBrowser.Enabled = false;
			}
		}

		private void btnSendCmd_Click(object sender, EventArgs e)
		{
			UInt16 data = 0;
			string strSend = null;            
			int i = 0;
			DateTime now;

			if ("" != tbHeader.Text)
			{
				cmd[0] = Convert.ToByte(tbHeader.Text, 16);
			}

			if ("" != tbCmd.Text)
			{
				cmd[1] = Convert.ToByte(tbCmd.Text, 16);
			}

			if ("" != tbData.Text)
			{
				data = Convert.ToUInt16(tbData.Text, 16);
				cmd[2] = (byte)(data >> 8);
				cmd[3] = (byte)data;
			}

			for (i = 0; i < 4; i++)
			{
				strSend += cmd[i].ToString("X2");
			}

			 now = DateTime.Now;
			 tbScreen.Text += "[" + now.ToString("yyyy-MM-dd hh:mm:ss") + "]  " + "Send:  " + "0x" + strSend + "\r\n";
		   
   
			_serialPort.Write(cmd, 0, 4);
			
		}

		private void btnFileBrowser_Click(object sender, EventArgs e)
		{
			string filePath = "";
			OpenFileDialog op = new OpenFileDialog();
			op.Filter = "Bin Files(*.bin)|*.bin|All Files(*.*)|*.*";
			if (op.ShowDialog() == DialogResult.OK)
			{
				filePath = op.FileName;
			}
			else
			{
				filePath = "";
			}
			tbFilePath.Text = filePath;

		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			tbScreen.Text = "";
		}

		private void tbScreen_TextChanged(object sender, EventArgs e)
		{
			tbScreen.SelectionStart = tbScreen.Text.Length;
			tbScreen.ScrollToCaret();
		}

		

		private void btnFileSend_Click(object sender, EventArgs e)
		{
			string fileName = tbFilePath.Text;
			CmdData fileCmd;
			int i = 0;
			string strSend = "";
			DateTime now;

			try
			{
				s_binFs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
			}
			catch (System.IO.FileNotFoundException)
			{
                MessageBox.Show(this, fileName + " can not be found!!");
				return;
			}

			s_binReader = new BinaryReader(s_binFs);


			segTotal = (byte)(s_binFs.Length / 1024);
			if ((s_binFs.Length % 1024) != 0)
				segTotal++;

			//for (tmp = 0; tmp < 8; tmp++)
			//{
			//    BinaryReader binReader = new BinaryReader(binFs);
			//    t = binReader.ReadBytes(2);
			//    tbScreen.Text += t[0].ToString("X2") + t[1].ToString("X2") + "\r\n";
			//    binReader.Close();
			//}              

			fileCmd.Header = 0xAA;
			fileCmd.Cmd = (byte)EnumOperation.Flash;
			fileCmd.Data = ((UInt16)EnumOperation.Bengin) << 8;

			SerialSend(fileCmd);

			for (i = 0; i < 4; i++)
			{
				strSend += cmd[i].ToString("X2");
			}

			now = DateTime.Now;
			tbScreen.Text += "[" + now.ToString("yyyy-MM-dd hh:mm:ss") + "]  " + "Send:  " + "0x" + strSend + "\r\n";

		}

		private void SinoSerial_Load(object sender, EventArgs e)
		{
			int i = 0;
			string[] str = SerialPort.GetPortNames();  
			if (str == null)  
			{
                MessageBox.Show(this, "No Serial Port is found!!");  
			   return;  
			} 

		   foreach (string s in SerialPort.GetPortNames())
		   {
				i++;
				cbPortName.Items.Add(s);
		   }
			if( i>=2 )
				cbPortName.SelectedIndex = 1;
			else
				cbPortName.SelectedIndex = 0;
		}

        private void cbPortName_click(object sender, EventArgs e)
        {
            int i = 0;
            string[] str = SerialPort.GetPortNames();
            if (str == null)
            {
                MessageBox.Show(this, "No Serial Port is found!!");
                return;
            }

            cbPortName.Items.Clear();

            foreach (string s in SerialPort.GetPortNames())
            {
                i++;
                cbPortName.Items.Add(s);
            }
            if (i >= 2)
                cbPortName.SelectedIndex = 1;
            else
                cbPortName.SelectedIndex = 0;

        }

        //protected override void WndProc(ref Message m)
        //{

        //    if (m.Msg == 0x0219)
        //    {
        //        //串口被拔出 
        //        if (m.WParam.ToInt32() == 0x8004)
        //        {
        //            MessageBox.Show("串口断开！系统将关闭！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            System.Threading.Thread.CurrentThread.Abort();
        //            System.Diagnostics.Process.GetCurrentProcess().Kill();
        //        }
        //    }
        //    base.WndProc(ref m);
        //}
	}
}
