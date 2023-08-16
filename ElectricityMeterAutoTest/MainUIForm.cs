using BaylanElectricAutoTest.Business;
using BaylanElectricAutoTest.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace BaylanElectricAutoTest
{

    public partial class MainUIForm : Form
    {
        public static int baudRate = 300;
        public static string portName = "";
        public static Thread Meter = null;
        public static Thread MeaTest = null;
        public static Thread ShortCutOffThread = null;
        public static Thread LongCutOffThread = null;
        private int saniye = 0;
        private int dakika = 0;
        private int saat = 0;
        public static List<ObisCode> obisCodes = new List<ObisCode>();
        public MainUIForm()
        {
            InitializeComponent();
            ShortCutOffThread = new Thread(new ThreadStart(ShortCutOffTest));
            LongCutOffThread = new Thread(new ThreadStart(LongCutOffTest));
        }

        private void MainUIForm_Load(object sender, EventArgs e)
        {
            InitializeObisCodes();
            foreach (var serialPort in SerialPort.GetPortNames())
            {
                SelectPort.Items.Add(serialPort);
            }
            SelectPort.SelectedIndex = 1;

            string[] baudrate = new string[7] { "300", "600", "1200", "2400", "4800", "9600", "19200" };
            BdPort.Items.AddRange(baudrate);
            BdPort.SelectedIndex = 5;
            LblSecond.Text = "00";
            LblMinute.Text = "00";
            LblHour.Text = "00";
        }

        private void InitializeObisCodes()
        {
            obisCodes.Add(new ObisCode("FinalObisReadDate", "R2", "0.9.2", "00"));
            obisCodes.Add(new ObisCode("FinalObisReadTime", "R2", "0.9.1", "00"));
            obisCodes.Add(new ObisCode("FinalObisReadCutOff_96.77.00*1", "R2", "96.77.00*1", "00"));
            obisCodes.Add(new ObisCode("FinalObisReadCutOff_96.77.0*1", "R2", "96.77.0*1", "00"));
            obisCodes.Add(new ObisCode("FinalObisReadCutOff_96.77.10*1", "R2", "96.77.10*1", "00"));
            obisCodes.Add(new ObisCode("FinalObisReadCutOff_96.77.1*1", "R2", "96.77.1*1", "00"));
            obisCodes.Add(new ObisCode("FinalObisReadCutOff_96.77.20*1", "R2", "96.77.20*1", "00"));
            obisCodes.Add(new ObisCode("FinalObisReadCutOff_96.77.2*1", "R2", "96.77.2*1", "00"));
            obisCodes.Add(new ObisCode("FinalObisReadCutOff_96.77.30*1", "R2", "96.77.30*1", "00"));
            obisCodes.Add(new ObisCode("FinalObisReadCutOff_96.77.3*1", "R2", "96.77.3*1", "00"));
        }

        public void ShortCutOffTest()
        {
            BasicRs232Setting();
            BasicShortCutOffMeatestExtendedModeSettings();
            ShortCutOffTesting();
        }

        public void LongCutOffTest()
        {
            BasicRs232Setting();
            BasicShortCutOffMeatestExtendedModeSettings();
            LongCutOffTesting();
        }

        /*public void StartMeaTest()
        {
            BasicRs232Setting();
            MeatestRem();
            Thread.Sleep(5000);
            MeatestRemIdn();
            Thread.Sleep(5000);
            PowerExtendedEnqueue();
            Thread.Sleep(5000);
            VoltageRExtendedSet("185");
            Thread.Sleep(5000);
            VoltageRExtendedSet("230");
            Thread.Sleep(5000);
            VoltageRExtendedSet("245");
        }*/
        private void ShortCutOffTestingButton_Click(object sender, EventArgs e)
        {
            StartTimer();
            listBox1.Items.Add("SHORT CUTOFF TESTING WAS START");
            if (!ShortCutOffThread.IsAlive)
            {
                baudRate = Convert.ToInt32(BdPort.SelectedItem);
                portName = SelectPort.SelectedItem.ToString();
                ShortCutOffThread = new Thread(new ThreadStart(ShortCutOffTest));
                ShortCutOffThread.Start();
            }
            else MessageBox.Show("SHORT CUTOFF Thread Çalışıyor", "Thread Çalışıyor", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LongCutOffTesting_Click(object sender, EventArgs e)
        {
            StartTimer();
            listBox1.Items.Add("LONG CUTOFF TESTING IS START");
            if (!LongCutOffThread.IsAlive)
            {
                baudRate = Convert.ToInt32(BdPort.SelectedItem);
                portName = SelectPort.SelectedItem.ToString();
                LongCutOffThread = new Thread(new ThreadStart(LongCutOffTest));
                LongCutOffThread.Start();
            }
            else MessageBox.Show("LongCutOffThread Çalışıyor", "Thread Çalışıyor", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void BasicOpticalSetting()
        {
            try
            {
                SerialClose(serialPort1);
                serialPort1.PortName = portName;
                serialPort1.BaudRate = baudRate;
                serialPort1.DataBits = 7;
                serialPort1.Parity = Parity.Even;
                serialPort1.StopBits = StopBits.One;
                //serialPort1.ReadTimeout = 1000;
                serialPort1.Open();
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("BasicOpticalSetting Hata: " + ex.Message)));
            }
            
        }

        public void BasicRs232Setting()
        {
            try
            {
                serialPort2.Close();
                serialPort2.PortName = "COM1";
                serialPort2.BaudRate = 9600;
                serialPort2.DataBits = 8;
                serialPort2.Parity = Parity.None;
                serialPort2.StopBits = StopBits.One;
                serialPort2.Handshake = Handshake.None;
                serialPort2.Open();
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("BasicRs232Setting Hata: " + ex.Message)));
            }

        }

        public void BasicShortCutOffMeatestExtendedModeSettings()
        {
            MeatestRem();
            Thread.Sleep(2000);
            MeatestRemIdn();
            Thread.Sleep(2000);
            VoltageRExtendedSet("230");
            Thread.Sleep(500);
            VoltageSExtendedSet("230");
            Thread.Sleep(500);
            VoltageTExtendedSet("230");
            Thread.Sleep(500);
            CurrentRExtendedSet("5");
            Thread.Sleep(500);
            CurrentSExtendedSet("5");
            Thread.Sleep(500);
            CurrentTExtendedSet("5");
            Thread.Sleep(500);
            VoltageRExtendedOn();
            Thread.Sleep(500);
            VoltageSExtendedOn();
            Thread.Sleep(500);
            VoltageTExtendedOn();
            Thread.Sleep(500);
            CurrentRExtendedOff();
            Thread.Sleep(500);
            CurrentSExtendedOff();
            Thread.Sleep(500);
            CurrentTExtendedOff();
            Thread.Sleep(500);
        }

        public void BasicMeterCommunicationProcesses()
        {
            ReadFlag();
            Thread.Sleep(200);
            ReadSerialNumber();
            Thread.Sleep(200);
        }

        public void ShortCutOffTesting()
        {
            /*for (int i = 0; i < 1; i++)
            {
                OperOn();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("MEATEST AÇILDI 5 SN BEKLEYECEK")));
                Thread.Sleep(5000);
                BasicOpticalSetting();
                BasicMeterCommunicationProcesses();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("OPTİK İSLEMLER YAPILDI 1SN BEKLEYECEK")));
                Thread.Sleep(1000);
                Read3phaseCutoff("FinalObisReadCutOff_96.77.00*1");
                OperOff();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("MEATEST KAPANDI 5 SN BEKLEYECEK")));
                Thread.Sleep(5000);
                OperOn();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("MEATEST AÇILDI 5 SN BEKLEYECEK")));
                Thread.Sleep(5000);
                BasicOpticalSetting();
                BasicMeterCommunicationProcesses();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("OPTİK İSLEMLER YAPILDI 1SN BEKLEYECEK")));
                Thread.Sleep(1000);
                Read3phaseCutoff("FinalObisReadCutOff_96.77.00*1");
                OperOff();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("MEATEST KAPANDI 10 SN BEKLEYECEK")));
                Thread.Sleep(10000);
                OperOn();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("MEATEST KAPANDI 5 SN BEKLEYECEK")));
                Thread.Sleep(5000);
                BasicOpticalSetting();
                BasicMeterCommunicationProcesses();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("OPTİK İSLEMLER YAPILDI 1SN BEKLEYECEK")));
                Thread.Sleep(1000);
                Read3phaseCutoff("FinalObisReadCutOff_96.77.00*1");
                OperOff();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("MEATEST KAPANDI 15 SN BEKLEYECEK")));
                Thread.Sleep(15000);
                OperOn();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("MEATEST AÇILDI 5 SN BEKLEYECEK")));
                Thread.Sleep(5000);
                BasicOpticalSetting();
                BasicMeterCommunicationProcesses();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("OPTİK İSLEMLER YAPILDI 1SN BEKLEYECEK")));
                Thread.Sleep(1000);
                Read3phaseCutoff("FinalObisReadCutOff_96.77.00*1");
                Thread.Sleep(1000);
                OperOff();
                Thread.Sleep(2000);
            }*/

            for (int j = 0; j < 1; j++)
            {
                OperOn();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("MEATEST AÇILDI 5 SN BEKLEYECEK")));
                Thread.Sleep(5000);
                Invoke((MethodInvoker)(() => listBox1.Items.Add("OPTİK İSLEMLER YAPILDI 1SN BEKLEYECEK")));
                Thread.Sleep(1000);
                VoltageExtendedOff("PACE:VOLT1:ENAB OFF\n");
                Invoke((MethodInvoker)(() => listBox1.Items.Add("R Fazı Kapandı 5 SN BEKLEYECEK")));
                Thread.Sleep(5000);
                VoltageExtendedOff("PACE:VOLT2:ENAB OFF\n");
                Invoke((MethodInvoker)(() => listBox1.Items.Add("S Fazı Kapandı 5 SN BEKLEYECEK")));
                Thread.Sleep(5000);
                VoltageExtendedOff("PACE:VOLT1:ENAB ON\n");
                Invoke((MethodInvoker)(() => listBox1.Items.Add("R Fazı Açıldı 5 SN BEKLEYECEK")));
                BasicOpticalSetting();
                Thread.Sleep(5000);
                BasicMeterCommunicationProcesses();
                //ReadRphaseCutoff("FinalObisReadCutOff_96.77.10*1");
                ReadPowerCut("FinalObisReadCutOff_96.77.10*1");
                VoltageExtendedOff("PACE:VOLT3:ENAB OFF\n");
                Invoke((MethodInvoker)(() => listBox1.Items.Add("T Fazı Kapandı 5 SN BEKLEYECEK")));
                Thread.Sleep(5000);
                VoltageExtendedOff("PACE:VOLT2:ENAB ON\n");
                Invoke((MethodInvoker)(() => listBox1.Items.Add("S Fazı Açıldı 5 SN BEKLEYECEK")));
                Thread.Sleep(5000);
                BasicMeterCommunicationProcesses();
                //ReadSphaseCutoff("FinalObisReadCutOff_96.77.20*1");
                ReadPowerCut("FinalObisReadCutOff_96.77.20*1");
                VoltageExtendedOff("PACE:VOLT3:ENAB ON\n");
                Invoke((MethodInvoker)(() => listBox1.Items.Add("T Fazı Açıldı 5 SN BEKLEYECEK")));
                Thread.Sleep(5000);
                BasicMeterCommunicationProcesses();
                ReadPowerCut("FinalObisReadCutOff_96.77.30*1");
                OperOff();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("TEST BİTTİ")));
            }

        }

        public void LongCutOffTesting()
        {
            for (int i = 0; i < 3; i++)
            {
                OperOn();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("MEATEST AÇILDI 5 SN BEKLEYECEK")));
                Thread.Sleep(5000);
                BasicOpticalSetting();
                BasicMeterCommunicationProcesses();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("OPTİK İSLEMLER YAPILDI 1SN BEKLEYECEK")));
                Thread.Sleep(1000);
                OperOff();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("MEATEST KAPANDI 3 DK BEKLEYECEK")));
                Thread.Sleep(190000);
                OperOn();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("MEATEST AÇILDI 5 SN BEKLEYECEK")));
                Thread.Sleep(5000);
                BasicOpticalSetting();
                BasicMeterCommunicationProcesses();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("OPTİK İSLEMLER YAPILDI 1SN BEKLEYECEK")));
                Thread.Sleep(1000);
                ReadPowerCut("FinalObisReadCutOff_96.77.0*1");
                OperOff();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("MEATEST KAPANDI 3DK 30SN SN BEKLEYECEK")));
                Thread.Sleep(220000);
                OperOn();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("MEATEST KAPANDI 5 SN BEKLEYECEK")));
                Thread.Sleep(5000);
                BasicOpticalSetting();
                BasicMeterCommunicationProcesses();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("OPTİK İSLEMLER YAPILDI 1SN BEKLEYECEK")));
                Thread.Sleep(1000);
                ReadPowerCut("FinalObisReadCutOff_96.77.0*1");
                OperOff();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("MEATEST KAPANDI 4 DK BEKLEYECEK")));
                Thread.Sleep(250000);
                OperOn();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("MEATEST AÇILDI 5 SN BEKLEYECEK")));
                Thread.Sleep(5000);
                BasicOpticalSetting();
                BasicMeterCommunicationProcesses();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("OPTİK İSLEMLER YAPILDI 1SN BEKLEYECEK")));
                Thread.Sleep(1000);
                ReadPowerCut("FinalObisReadCutOff_96.77.0*1");
                Thread.Sleep(1000);
                OperOff();
                Thread.Sleep(2000);
            }

            for (int j = 0; j < 10; j++)
            {
                OperOn();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("MEATEST AÇILDI 5 SN BEKLEYECEK")));
                Thread.Sleep(5000);
                Invoke((MethodInvoker)(() => listBox1.Items.Add("OPTİK İSLEMLER YAPILDI 1SN BEKLEYECEK")));
                Thread.Sleep(1000);
                VoltageExtendedOff("PACE:VOLT1:ENAB OFF\n");
                Invoke((MethodInvoker)(() => listBox1.Items.Add("R Fazı Kapandı 5 SN BEKLEYECEK")));
                Thread.Sleep(5000);
                VoltageExtendedOff("PACE:VOLT2:ENAB OFF\n");
                Invoke((MethodInvoker)(() => listBox1.Items.Add("S Fazı Kapandı 5 SN BEKLEYECEK")));
                Thread.Sleep(5000);
                VoltageExtendedOff("PACE:VOLT1:ENAB ON\n");
                Invoke((MethodInvoker)(() => listBox1.Items.Add("R Fazı Açıldı 5 SN BEKLEYECEK")));
                BasicOpticalSetting();
                Thread.Sleep(5000);
                BasicMeterCommunicationProcesses();
                ReadRphaseCutoff("FinalObisReadCutOff_96.77.10*1");
                VoltageExtendedOff("PACE:VOLT3:ENAB OFF\n");
                Invoke((MethodInvoker)(() => listBox1.Items.Add("T Fazı Kapandı 5 SN BEKLEYECEK")));
                Thread.Sleep(190000);
                VoltageExtendedOff("PACE:VOLT2:ENAB ON\n");
                Invoke((MethodInvoker)(() => listBox1.Items.Add("S Fazı Açıldı 5 SN BEKLEYECEK")));
                Thread.Sleep(190000);
                BasicMeterCommunicationProcesses();
                ReadSphaseCutoff("FinalObisReadCutOff_96.77.20*1");
                VoltageExtendedOff("PACE:VOLT3:ENAB ON\n");
                Invoke((MethodInvoker)(() => listBox1.Items.Add("T Fazı Açıldı 5 SN BEKLEYECEK")));
                Thread.Sleep(190000);
                BasicMeterCommunicationProcesses();
                ReadPowerCut("FinalObisReadCutOff_96.77.30*1");
                OperOff();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("TEST BİTTİ")));
            }

        }

        private void SelectPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectPort.SelectedItem != null)
            {
                string selectedItem_Model = SelectPort.SelectedItem.ToString();
                MessageBox.Show("Selected item: " + selectedItem_Model);
            }
        }
        public void SerialClose(SerialPort sp)
        {
            sp.Close();
        }
        public void ReadFlag()
        {
            try
            {
                string receivedData = "";
                receivedData = OpticalWriteAndRead(300, 300, 200, "/?!\r\n",true);
                Invoke((MethodInvoker)(() => listBox1.Items.Add("ReadFlag: Write islemi yapildi")));
                Invoke((MethodInvoker)(() => listBox1.Items.Add(receivedData)));
                if (receivedData != null && receivedData.Length > 0)
                {
                    Invoke((MethodInvoker)(() => listBox1.Items.Add("Flag okuma basarılı")));
                    //listBox1.Items.Add("Remote modu açıldı");
                }
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("ReadFlag Hata: " + ex.Message)));
            }
        }

        public void ReadSerialNumber()
        {
            try
            {

                string receivedData = "";
                receivedData = OpticalWriteAndRead(300, 9600, 200, "\u0006051\r\n",true);
                Invoke((MethodInvoker)(() => listBox1.Items.Add("ReadSerial: Write islemi yapildi")));
                Invoke((MethodInvoker)(() => listBox1.Items.Add(receivedData)));
                if (receivedData != null && receivedData.Length > 0)
                {
                    Invoke((MethodInvoker)(() => listBox1.Items.Add("Serial okuma basarılı")));
                }
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("ReadSerial Hata: " + ex.Message)));
            }
        }

        
        public void ReadDate()
        {
            try
            {
                string receivedData = "";
                receivedData = OpticalWriteAndRead(9600, 9600, 200, CreateObisCommand(obisCodes.Where(x => x.Name == "FinalObisReadDate").FirstOrDefault()), true);
                serialPort1.BaudRate = baudRate;
                Invoke((MethodInvoker)(() => listBox1.Items.Add("ReadDate: Write islemi yapildi")));
                Invoke((MethodInvoker)(() => listBox1.Items.Add(receivedData)));
                if (receivedData != null && receivedData.Length > 0)
                {
                    Invoke((MethodInvoker)(() => listBox1.Items.Add("ReadDate okuma basarılı")));
                }
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("ReadDate Hata: " + ex.Message)));
            }
        }

        public void ReadTime()
        {
            try
            {

                string receivedData = "";
                receivedData = OpticalWriteAndRead(9600, 9600, 200, CreateObisCommand(obisCodes.Where(x => x.Name == "FinalObisReadTime").FirstOrDefault()), true);
                serialPort1.BaudRate = baudRate;
                serialPort1.BaudRate = baudRate;
                Invoke((MethodInvoker)(() => listBox1.Items.Add("ReadTime: Write islemi yapildi")));
                Invoke((MethodInvoker)(() => listBox1.Items.Add(receivedData)));
                if (receivedData != null && receivedData.Length > 0)
                {
                    Invoke((MethodInvoker)(() => listBox1.Items.Add("ReadTime okuma basarılı")));
                }
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("ReadTime Hata: " + ex.Message)));
            }
        }
        public void ReadPowerCut(string CutType)
        {
            try
            {
                string receivedData = "";
                receivedData = OpticalWriteAndRead(9600, 9600, 200, CreateObisCommand(obisCodes.Where(x => x.Name == CutType).FirstOrDefault()), true);
                serialPort1.BaudRate = baudRate;
                Invoke((MethodInvoker)(() => listBox1.Items.Add("Read3phaseCutoff: Write islemi yapildi")));
                Invoke((MethodInvoker)(() => listBox1.Items.Add(receivedData)));
                if (receivedData != null && receivedData.Length > 0)
                {
                    var resultStartDateTime = Parse3phaseShortCutoffDateTime(receivedData, 0);
                    Invoke((MethodInvoker)(() => listBox1.Items.Add("Kesinti başlangıç tarihi :" + resultStartDateTime)));
                    var resultFinishDateTime = Parse3phaseShortCutoffDateTime(receivedData, 1);
                    Invoke((MethodInvoker)(() => listBox1.Items.Add("Kesinti bitiş tarihi :" + resultFinishDateTime)));
                    TimeSpan difference = DateDifferenceCalculator.CalculateDifference(resultStartDateTime, resultFinishDateTime);
                    Invoke((MethodInvoker)(() => listBox1.Items.Add("Diffrence of Dates :" + difference)));
                    double TotalMinutes = difference.TotalMinutes;
                    if (TotalMinutes == 0 || TotalMinutes == 1 || TotalMinutes == 2)
                    {
                        if (CutType == "FinalObisReadCutOff_96.77.00*1")
                        {
                            Invoke((MethodInvoker)(() => listBox1.Items.Add("3 Faz Kısa kesinti kaydı basarılı")));
                        }
                        else if (CutType == "FinalObisReadCutOff_96.77.10*1")
                        {
                            Invoke((MethodInvoker)(() => listBox1.Items.Add("R Faz Kısa kesinti kaydı basarılı")));
                        }
                        else if (CutType == "FinalObisReadCutOff_96.77.20*1")
                        {
                            Invoke((MethodInvoker)(() => listBox1.Items.Add("S Faz Kısa kesinti kaydı basarılı")));
                        }
                        else if (CutType == "FinalObisReadCutOff_96.77.30*1")
                        {
                            Invoke((MethodInvoker)(() => listBox1.Items.Add("T Faz Kısa kesinti kaydı basarılı")));
                        }
                        else
                        {
                            Invoke((MethodInvoker)(() => listBox1.Items.Add("Kısa Kesinti Obis kodu eşitlenemedi")));
                        }
                    } 
                        
                    else if (TotalMinutes >= 3)
                    {
                        if (CutType == "FinalObisReadCutOff_96.77.0*1")
                        {
                            Invoke((MethodInvoker)(() => listBox1.Items.Add("3 Faz Uzun kesinti kaydı basarılı")));
                        }
                        else if (CutType == "FinalObisReadCutOff_96.77.1*1")
                        {
                            Invoke((MethodInvoker)(() => listBox1.Items.Add("R Faz Uzun kesinti kaydı basarılı")));
                        }
                        else if (CutType == "FinalObisReadCutOff_96.77.2*1")
                        {
                            Invoke((MethodInvoker)(() => listBox1.Items.Add("S Faz Uzun kesinti kaydı basarılı")));
                        }
                        else if (CutType == "FinalObisReadCutOff_96.77.3*1")
                        {
                            Invoke((MethodInvoker)(() => listBox1.Items.Add("T Faz Uzun kesinti kaydı basarılı")));
                        }
                        else
                        {
                            Invoke((MethodInvoker)(() => listBox1.Items.Add("Uzun Kesinti Obis kodu eşitlenemedi")));
                        }
                    }
                    else
                    {
                        Invoke((MethodInvoker)(() => listBox1.Items.Add("Read3phaseCutoff tarih farkı alınamadı")));
                    }
                }
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("Read3phaseCutoff Hata: " + ex.Message)));
            }
        }


        public void ReadRphaseCutoff(string CutOffType)
        {
            try
            {
                string receivedData = "";
                receivedData = OpticalWriteAndRead(9600, 9600, 200, CreateObisCommand(obisCodes.Where(x => x.Name == CutOffType).FirstOrDefault()), true);
                serialPort1.BaudRate = baudRate;
                Invoke((MethodInvoker)(() => listBox1.Items.Add("ReadRphaseCutoff: Write islemi yapildi")));
                Invoke((MethodInvoker)(() => listBox1.Items.Add(receivedData)));
                if (receivedData != null && receivedData.Length > 0)
                {
                    var resultStartDateTime = Parse3phaseShortCutoffDateTime(receivedData, 0);
                    Invoke((MethodInvoker)(() => listBox1.Items.Add("Kesinti başlangıç tarihi :" + resultStartDateTime)));
                    var resultFinishDateTime = Parse3phaseShortCutoffDateTime(receivedData, 1);
                    Invoke((MethodInvoker)(() => listBox1.Items.Add("Kesinti bitiş tarihi :" + resultFinishDateTime)));
                    TimeSpan difference = DateDifferenceCalculator.CalculateDifference(resultStartDateTime, resultFinishDateTime);
                    Invoke((MethodInvoker)(() => listBox1.Items.Add("Diffrence of Dates :" + difference)));
                    double TotalMinutes = difference.TotalMinutes;
                    if (TotalMinutes == 0 || TotalMinutes == 1 || TotalMinutes == 2)
                    {
                        Invoke((MethodInvoker)(() => listBox1.Items.Add("Kısa kesinti kaydı basarılı")));
                    }
                    else if (TotalMinutes >= 3)
                    {
                        Invoke((MethodInvoker)(() => listBox1.Items.Add("Uzun kesinti kaydı basarılı")));
                    }
                    else
                    {
                        Invoke((MethodInvoker)(() => listBox1.Items.Add("ReadRphaseCutoff tarih farkı alınamadı")));
                    }
                }
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("ReadRphaseCutoff Hata: " + ex.Message)));
            }
        }


        public void ReadSphaseCutoff(string CutOffType)
        {
            try
            {
                string receivedData = "";
                receivedData = OpticalWriteAndRead(9600, 9600, 200, CreateObisCommand(obisCodes.Where(x => x.Name == CutOffType).FirstOrDefault()), true);
                serialPort1.BaudRate = baudRate;
                Invoke((MethodInvoker)(() => listBox1.Items.Add("ReadSphaseCutoff: Write islemi yapildi")));
                Invoke((MethodInvoker)(() => listBox1.Items.Add(receivedData)));
                if (receivedData != null && receivedData.Length > 0)
                {
                    var resultStartDateTime = Parse3phaseShortCutoffDateTime(receivedData, 0);
                    Invoke((MethodInvoker)(() => listBox1.Items.Add("Kesinti başlangıç tarihi :" + resultStartDateTime)));
                    var resultFinishDateTime = Parse3phaseShortCutoffDateTime(receivedData, 1);
                    Invoke((MethodInvoker)(() => listBox1.Items.Add("Kesinti bitiş tarihi :" + resultFinishDateTime)));
                    TimeSpan difference = DateDifferenceCalculator.CalculateDifference(resultStartDateTime, resultFinishDateTime);
                    Invoke((MethodInvoker)(() => listBox1.Items.Add("Diffrence of Dates :" + difference)));
                    double TotalMinutes = difference.TotalMinutes;
                    if (TotalMinutes == 0 || TotalMinutes == 1 || TotalMinutes == 2)
                    {
                        Invoke((MethodInvoker)(() => listBox1.Items.Add("Kısa kesinti kaydı basarılı")));
                    }
                    else if (TotalMinutes >= 3)
                    {
                        Invoke((MethodInvoker)(() => listBox1.Items.Add("Uzun kesinti kaydı basarılı")));
                    }
                    else
                    {
                        Invoke((MethodInvoker)(() => listBox1.Items.Add("ReadSphaseCutoff tarih farkı alınamadı")));
                    }
                }
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("ReadSphaseCutoff Hata: " + ex.Message)));
            }
        }


        public void ReadTphaseCutoff(string CutOffType)
        {
            try
            {
                string receivedData = "";
                receivedData = OpticalWriteAndRead(9600, 9600, 200, CreateObisCommand(obisCodes.Where(x => x.Name == CutOffType).FirstOrDefault()), true);
                serialPort1.BaudRate = baudRate;
                Invoke((MethodInvoker)(() => listBox1.Items.Add("ReadTphaseCutoff: Write islemi yapildi")));
                Invoke((MethodInvoker)(() => listBox1.Items.Add(receivedData)));
                if (receivedData != null && receivedData.Length > 0)
                {
                    var resultStartDateTime = Parse3phaseShortCutoffDateTime(receivedData, 0);
                    Invoke((MethodInvoker)(() => listBox1.Items.Add("Kesinti başlangıç tarihi :" + resultStartDateTime)));
                    var resultFinishDateTime = Parse3phaseShortCutoffDateTime(receivedData, 1);
                    Invoke((MethodInvoker)(() => listBox1.Items.Add("Kesinti bitiş tarihi :" + resultFinishDateTime)));
                    TimeSpan difference = DateDifferenceCalculator.CalculateDifference(resultStartDateTime, resultFinishDateTime);
                    Invoke((MethodInvoker)(() => listBox1.Items.Add("Diffrence of Dates :" + difference)));
                    double TotalMinutes = difference.TotalMinutes;
                    if (TotalMinutes == 0 || TotalMinutes == 1 || TotalMinutes == 2)
                    {
                        Invoke((MethodInvoker)(() => listBox1.Items.Add("Kısa kesinti kaydı basarılı")));
                    }
                    else if (TotalMinutes >= 3)
                    {
                        Invoke((MethodInvoker)(() => listBox1.Items.Add("Uzun kesinti kaydı basarılı")));
                    }
                    else
                    {
                        Invoke((MethodInvoker)(() => listBox1.Items.Add("ReadTphaseCutoff tarih farkı alınamadı")));
                    }
                }
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("ReadTphaseCutoff Hata: " + ex.Message)));
            }
        }

        public class DateDifferenceCalculator
        {
            public static TimeSpan CalculateDifference(string date1Str, string date2Str)
            {
                DateTime date1 = DateTime.ParseExact(date1Str, "yy-MM-dd,HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                DateTime date2 = DateTime.ParseExact(date2Str, "yy-MM-dd,HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                // Calculate the time difference
                TimeSpan difference = date2 - date1;
                return difference;
            }
        }
        public string Parse3phaseShortCutoffDateTime(string input , int part)
        {
            try
            {
                int value = part;
                int startIndex = input.IndexOf("("); // Parantezin başlangıç dizini
                int endIndex = input.IndexOf(")"); // Parantezin bitiş dizini
                if (startIndex != -1 && endIndex != -1 && startIndex < endIndex)
                {
                    string timeString = input.Substring(startIndex + 1, endIndex - startIndex - 1);
                    string[] timeParts = timeString.Split(';');
                    string timeInfo = timeParts[value];
                    return timeInfo;
                }
            }

            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("Parse3phaseShortCutoffDateTime Hata: " + ex.Message)));
            }
            return null;
        }

        public string Parse3phaseShortCutoffStartYear(string input, int part)
        {
            try
            {
                int value = part;
                int startIndex = input.IndexOf("("); // Parantezin başlangıç dizini
                int endIndex = input.IndexOf("-"); // Parantezin bitiş dizini
                if (startIndex != -1 && endIndex != -1 && startIndex < endIndex)
                {
                    string insideParentheses = input.Substring(startIndex + 1, endIndex - startIndex - 1);
                    string[] parts = insideParentheses.Split(';');
                    if (parts.Length >= 1)
                    {
                        string firstPart = parts[part];
                        string firstNumberString = new string(firstPart.Trim().TakeWhile(char.IsDigit).ToArray());
                        return firstNumberString;
                    }

                }
            }

            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("Parse3phaseShortCutoffStartYear Hata: " + ex.Message)));
            }
            return null;
        }
        
        public string Parse3phaseShortCutoffStartMounth(string input)
        {
            try
            {
                string pattern = @"(?<=-)\d{2}";
                Match match = Regex.Match(input, pattern);
                if (match.Success)
                {
                    return match.Value;
                }
            }

            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("Parse3phaseShortCutoffStartMounth Hata: " + ex.Message)));
            }
            return null;
        }


        public string Parse3phaseShortCutoffStartDay(string input)
        {
            try
            {
                string pattern = @"(?<=-)\d{2}(?=\,)";
                Match match = Regex.Match(input, pattern);
                if (match.Success)
                {
                    return match.Value;
                }
            }

            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("Parse3phaseShortCutoffStartDay Hata: " + ex.Message)));
            }
            return null;
        }

        public string Parse3phaseShortCutoffStartHour(string input)
        {
            try
            {
                string pattern = @"(?<=,)\d{2}(?=\:)";
                Match match = Regex.Match(input, pattern);
                if (match.Success)
                {
                    return match.Value;
                }
            }

            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("Parse3phaseShortCutoffStartHour Hata: " + ex.Message)));
            }
            return null;
        }

        public string Parse3phaseShortCutoffStartMinute(string input)
        {
            try
            {
                int startIndex = input.IndexOf(":"); // Parantezin başlangıç dizini
                int endIndex = startIndex + 3; // Parantezin bitiş dizini
                if (startIndex != -1 && endIndex != -1 && startIndex < endIndex)
                {
                    string insideParentheses = input.Substring(startIndex + 1, endIndex - startIndex - 1);
                    string[] parts = insideParentheses.Split(';');
                    if (parts.Length >= 1)
                    {
                        string firstPart = parts[0];
                        string firstNumberString = new string(firstPart.Trim().TakeWhile(char.IsDigit).ToArray());
                        return firstNumberString;
                    }

                }
            }

            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("Parse3phaseShortCutoffStartMinute Hata: " + ex.Message)));
            }
            return null;
        }

        public string Parse3phaseShortCutoffFinishMinute(string input)
        {
            try
            {
                int startIndex = input.IndexOf("(");
                int endIndex = input.IndexOf(")");
                string numbersString = input.Substring(startIndex + 1, endIndex - startIndex - 1);
                {
                    string insideParentheses = input.Substring(startIndex + 1, endIndex - startIndex - 1);
                    string[] parts = insideParentheses.Split(';');
                    if (parts.Length >= 1)
                    {
                        string firstPart = parts[0];
                        string firstNumberString = new string(firstPart.Trim().TakeWhile(char.IsDigit).ToArray());
                        return firstNumberString;
                    }

                }
            }

            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("Parse3phaseShortCutoffStartMinute Hata: " + ex.Message)));
            }
            return null;
        }

        public int StringToInt(string value)
        {
            try
            {
                if (int.TryParse(value, out int resultStringToInt))
                
                {
                    return resultStringToInt;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("StringToInt: " + ex.Message)));
                return 0;
            }
        }

        public string OpticalWriteAndRead(int WriteBaudRate, int ReadBaudRate, int delay, string command,bool AddLog)
        {
            serialPort1.BaudRate = WriteBaudRate;
            SerialWrite(serialPort1, command);
            Thread.Sleep(delay);
            serialPort1.BaudRate = ReadBaudRate;
            string answer = SerialRead(1000, serialPort1, AddLog);
            return answer;
        }
        private void SerialWrite(SerialPort serialPort1, string command)
        {
            Invoke((MethodInvoker)(() => listBox1.Items.Add("Serial Command: " + command)));
            serialPort1.Write(command);
        }

        private string SerialRead(int readTimeout, SerialPort ChoseSerialPort, bool AddLog)
        {
            var startDateTime = DateTime.Now;
            serialPort1.ReadTimeout = readTimeout;
            string receivedData = "";
            try
            {
                while (DateTime.Now.Subtract(startDateTime).TotalMilliseconds < serialPort1.ReadTimeout)
                {
                    receivedData += ChoseSerialPort.ReadExisting();
                }
                if (AddLog == true)
                {
                    Invoke((MethodInvoker)(() => listBox1.Items.Add("Received Data: " + receivedData)));
                }
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("ReadSerial Data dönmedi: " + ex.Message)));
            }
            return receivedData;
        }
        public string CreateObisCommand(ObisCode obis)
        {
            string BeforeBccCalObis = obis.Mode + "\u0002" + obis.Code + "(" + obis.Parameter + ")" + "\u0003";
            string AfterBccCalObis = "\u0001" + obis.Mode + "\u0002" + obis.Code + "(" + obis.Parameter + ")" + "\u0003" + Convert.ToChar(CalculateBcc(BeforeBccCalObis)) + "\r\n";
            return AfterBccCalObis;
        }

        public static byte CalculateBcc(string BccParameter)
        {
            byte bcc = 0;
            byte[] res = Encoding.UTF8.GetBytes(BccParameter);
            for (int j = 0; j < res.Length; j++)
            {
                bcc ^= res[j];
            }
            return bcc;
        }
       

        public void MeatestRemIdn()
        {
            try
            {
                SerialWrite(serialPort2, "SYST:REM;*IDN?\n");
                string receivedData = "";
                receivedData = SerialRead(1000, serialPort2, false);
                //listBox1.Items.Add("MeatestRemIdn Write islemi yapildi");
                //listBox1.Items.Add(receivedData);
                Invoke((MethodInvoker)(() => listBox1.Items.Add("MeatestRemIdn Write islemi yapildi")));
                Invoke((MethodInvoker)(() => listBox1.Items.Add(receivedData)));

                if (receivedData != null && receivedData.Length > 0)
                {
                    Invoke((MethodInvoker)(() => listBox1.Items.Add("Remote modu açıldı")));
                    //listBox1.Items.Add("Remote modu açıldı");
                }
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("MeatestRemIdn Hata: " + ex.Message)));
            }
        }
        public void OperOn()
        {
            SerialWrite(serialPort2, "OUTP ON\n");
        }

        public void OperOff()
        {
            SerialWrite(serialPort2, "OUTP OFF\n");
        }
        public void OperSituation()
        {
            SerialWrite(serialPort2, "OUTP?\n");
            //string receivedData = serialPort1.ReadLine();
            string receivedData = SerialRead(1000, serialPort2, false);
            Invoke((MethodInvoker)(() => listBox1.Items.Add(receivedData)));
            //listBox1.Items.Add(receivedData);
            if (receivedData == "OFF")
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("Remote modu açıldı")));
                //listBox1.Items.Add("Remote modu açıldı");
            }
        }

        public void MeatestRem()
        {
            try
            {
                SerialWrite(serialPort2, "\nSYST:REM\n");
                //listBox1.Items.Add("MeatestRemIdn Write islemi yapildi")
                Invoke((MethodInvoker)(() => listBox1.Items.Add("MeatestRemIdn Write islemi yapildi")));
            }
            catch (Exception ex)
            {

                Invoke((MethodInvoker)(() => listBox1.Items.Add("MeatestRemIdn: " + ex.Message)));
            }

        }

        public void PowerEnqueue()
        {
            SerialWrite(serialPort2, "PAC:POW?\n");
            string receivedData = SerialRead(1000, serialPort2, false);
            Invoke((MethodInvoker)(() => listBox1.Items.Add(receivedData)));
            //listBox1.Items.Add(receivedData);
            if (receivedData == "OFF")
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("Remote modu açıldı")));
                // listBox1.Items.Add("Remote modu açıldı");
            }
        }

        public void PowerExtendedEnqueue()
        {
            try
            {
                SerialWrite(serialPort1, "PACE:POW?\n");
                string receivedData = SerialRead(1000,serialPort2, false);
                Invoke((MethodInvoker)(() => listBox1.Items.Add(receivedData)));
                //listBox1.Items.Add(receivedData);
                var result = ConvertExpoToDec(receivedData);
                if (!result) Invoke((MethodInvoker)(() => listBox1.Items.Add("ConvertExpoToDec Parse Hatası")));
            }
            catch (Exception ex)
            {

                Invoke((MethodInvoker)(() => listBox1.Items.Add("PowerExtendedEnqueue Hata: " + ex.Message)));
            }

        }

        public bool ConvertExpoToDec(string input)
        {

            bool result = false;
            try
            {
                //double doubleValue = double.Parse(input, NumberStyles.Float, CultureInfo.InvariantCulture);
                result = double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double doubleValue);
                if (result)
                {
                    decimal decimalValue = (decimal)doubleValue;
                    string valueResult = decimalValue.ToString();
                    Invoke((MethodInvoker)(() => listBox1.Items.Add(valueResult + "W")));
                }

            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("ConvertExpoToDec Hata: " + ex.Message)));
            }

            return result;
        }

        public void VoltageExtendedOff(string VCutOff)
        {
            
            SerialWrite(serialPort2, VCutOff);
            SerialRead(1000, serialPort2, false);
            //string receivedData = SerialRead(1000, serialPort2);
            //Invoke((MethodInvoker)(() => listBox1.Items.Add(receivedData)));
            //listBox1.Items.Add(receivedData);
            //var result = ConvertExpoToDec(receivedData);
            //if (!result) Invoke((MethodInvoker)(() => listBox1.Items.Add("ConvertExpoToDec Parse Hatası")));
        }

        public void VoltageRExtendedOn()
        {
            SerialWrite(serialPort2, "PACE:VOLT1:ENAB ON\n");
            SerialRead(1000, serialPort2, false);
            //string receivedData = SerialRead(1000, serialPort2);
            //Invoke((MethodInvoker)(() => listBox1.Items.Add(receivedData)));
            //listBox1.Items.Add(receivedData);
            //var result = ConvertExpoToDec(receivedData);
            //if (!result) Invoke((MethodInvoker)(() => listBox1.Items.Add("ConvertExpoToDec Parse Hatası")));
        }

        public void VoltageRExtendedSet(string volt)
        {
            try
            {
                SerialWrite(serialPort2, "PACE:VOLT1 " + volt + "\n");
                Invoke((MethodInvoker)(() => listBox1.Items.Add("PACE:VOLT1 :" + volt + "V")));
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("VoltageRExtendedSet Hata: " + ex.Message)));
            }
        }

        public void PhaseExtendedSet(string voltage ,string ankle)
        {
            try
            {
                SerialWrite(serialPort2, "PACE:" + voltage + "PHAS " + ankle + "\n");
                Invoke((MethodInvoker)(() => listBox1.Items.Add("PACE:" + voltage + "V" + "PHAS " + ankle + "°")));
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("PhaseRExtendedSet Hata: " + ex.Message)));
            }
        }
        public void VoltageSExtendedOff()
        {
            SerialWrite(serialPort2, "PACE:VOLT1:ENAB OFF\n");
            SerialRead(1000, serialPort2, false);
            //string receivedData = SerialRead(1000, serialPort2);
            //Invoke((MethodInvoker)(() => listBox1.Items.Add(receivedData)));
            //var result = ConvertExpoToDec(receivedData);
            //if (!result) Invoke((MethodInvoker)(() => listBox1.Items.Add("ConvertExpoToDec Parse Hatası")));
        }
        public void VoltageSExtendedOn()
        {
            SerialWrite(serialPort2, "PACE:VOLT2:ENAB ON\n");
            SerialRead(1000, serialPort2, false);
            //string receivedData = SerialRead(1000, serialPort2);
            //Invoke((MethodInvoker)(() => listBox1.Items.Add(receivedData)));
            //listBox1.Items.Add(receivedData);
            //var result = ConvertExpoToDec(receivedData);
            //if (!result) Invoke((MethodInvoker)(() => listBox1.Items.Add("ConvertExpoToDec Parse Hatası")));
        }

        public void VoltageSExtendedSet(string volt)
        {
            try
            {
                SerialWrite(serialPort2, "PACE:VOLT2 " + volt + "\n");
                Invoke((MethodInvoker)(() => listBox1.Items.Add("PACE:VOLT2 :" + volt + "V")));
                //listBox1.Items.Add(receivedData);
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("VoltageSExtendedSet Hata: " + ex.Message)));
            }
        }
        public void VoltageTExtendedOff()
        {
            SerialWrite(serialPort2, "PACE:VOLT3:ENAB OFF\n");
            SerialRead(1000, serialPort2, false);
            //string receivedData = SerialRead(1000, serialPort2);
            //Invoke((MethodInvoker)(() => listBox1.Items.Add(receivedData)));
            //listBox1.Items.Add(receivedData);
            //var result = ConvertExpoToDec(receivedData);
            //if (!result) Invoke((MethodInvoker)(() => listBox1.Items.Add("VoltageTExtendedOff Parse Hatası")));
        }
        public void VoltageTExtendedOn()
        {
            SerialWrite(serialPort2, "PACE:VOLT3:ENAB ON\n");
            SerialRead(1000, serialPort2, false);
            //string receivedData = SerialRead(1000, serialPort2);
            //Invoke((MethodInvoker)(() => listBox1.Items.Add(receivedData)));
            //listBox1.Items.Add(receivedData);
            //var result = ConvertExpoToDec(receivedData);
            //if (!result) Invoke((MethodInvoker)(() => listBox1.Items.Add("VoltageTExtendedOn Parse Hatası")));
        }

        public void VoltageTExtendedSet(string volt)
        {
            try
            {
                SerialWrite(serialPort2, "PACE:VOLT3 " + volt + "\n");
                //string receivedData = ReadSerial();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("PACE:VOLT3 :" + volt + "V")));
                //listBox1.Items.Add(receivedData);
                //var result = ConvertExpoToDec(receivedData);
                //if (!result) Invoke((MethodInvoker)(() => listBox1.Items.Add("ConvertExpoToDec Parse Hatası")));
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("VoltageTExtendedSet Hata: " + ex.Message)));
            }
        }


        public void CurrentRExtendedOff()
        {
            SerialWrite(serialPort2, "PACE:CURR1:ENAB OFF\n");
            SerialRead(1000, serialPort2, false);
            //string receivedData = SerialRead(1000, serialPort2);
            //Invoke((MethodInvoker)(() => listBox1.Items.Add(receivedData)));
            //listBox1.Items.Add(receivedData);
            //var result = ConvertExpoToDec(receivedData);
            //if (!result) Invoke((MethodInvoker)(() => listBox1.Items.Add("CurrentRExtendedOff Parse Hatası")));
        }
        public void CurrentRExtendedOn()
        {
            SerialWrite(serialPort2, "PACE:CURR1:ENAB ON\n");
            SerialRead(1000, serialPort2, false);
            //string receivedData = SerialRead(1000, serialPort2);
            //Invoke((MethodInvoker)(() => listBox1.Items.Add(receivedData)));
            //listBox1.Items.Add(receivedData);
            //var result = ConvertExpoToDec(receivedData);
            //if (!result) Invoke((MethodInvoker)(() => listBox1.Items.Add("CurrentRExtendedOn Parse Hatası")));
        }

        public void CurrentRExtendedSet(string current)
        {
            try
            {
                SerialWrite(serialPort2, "PACE:CURR1 " + current + "\n");
                //string receivedData = ReadSerial();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("PACE:CURR1 :" + current + "A")));
                //listBox1.Items.Add(receivedData);
                //var result = ConvertExpoToDec(receivedData);
                //if (!result) Invoke((MethodInvoker)(() => listBox1.Items.Add("ConvertExpoToDec Parse Hatası")));
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("CurrentRExtendedSet Hata: " + ex.Message)));
            }
        }


        public void CurrentSExtendedOff()
        {
            SerialWrite(serialPort2, "PACE:CURR2:ENAB OFF\n");
            SerialRead(1000, serialPort2, false);
            //string receivedData = SerialRead(1000, serialPort2);
            //Invoke((MethodInvoker)(() => listBox1.Items.Add(receivedData)));
            //listBox1.Items.Add(receivedData);
            //var result = ConvertExpoToDec(receivedData);
            //if (!result) Invoke((MethodInvoker)(() => listBox1.Items.Add("CurrentSExtendedOff Parse Hatası")));
        }
        public void CurrentSExtendedOn()
        {
            SerialWrite(serialPort2, "PACE:CURR2:ENAB ON\n");
            SerialRead(1000, serialPort2, false);
            //string receivedData = SerialRead(1000, serialPort2);
            //Invoke((MethodInvoker)(() => listBox1.Items.Add(receivedData)));
            //listBox1.Items.Add(receivedData);
            //var result = ConvertExpoToDec(receivedData);
            //if (!result) Invoke((MethodInvoker)(() => listBox1.Items.Add("CurrentSExtendedOn Parse Hatası")));
        }

        public void CurrentSExtendedSet(string current)
        {
            try
            {
                SerialWrite(serialPort2, "PACE:CURR2 " + current + "\n");
                //string receivedData = ReadSerial();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("PACE:CURR2 :" + current + "A")));
                //listBox1.Items.Add(receivedData);
                //var result = ConvertExpoToDec(receivedData);
                //if (!result) Invoke((MethodInvoker)(() => listBox1.Items.Add("ConvertExpoToDec Parse Hatası")));
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("CurrentSExtendedSet Hata: " + ex.Message)));
            }
        }


        public void CurrentTExtendedOff()
        {
            SerialWrite(serialPort2, "PACE:CURR3:ENAB OFF\n");
            SerialRead(1000, serialPort2, false);
            //string receivedData = SerialRead(1000, serialPort2);
            //Invoke((MethodInvoker)(() => listBox1.Items.Add(receivedData)));
            //listBox1.Items.Add(receivedData);
            //var result = ConvertExpoToDec(receivedData);
            //if (!result) Invoke((MethodInvoker)(() => listBox1.Items.Add("CurrentTExtendedOff Parse Hatası")));
        }
        public void CurrentTExtendedOn()
        {
            SerialWrite(serialPort2, "PACE:CURR3:ENAB ON\n");
            SerialRead(1000, serialPort2, false);
            //string receivedData = SerialRead(1000, serialPort2);
            //Invoke((MethodInvoker)(() => listBox1.Items.Add(receivedData)));
            //listBox1.Items.Add(receivedData);
            //var result = ConvertExpoToDec(receivedData);
            //if (!result) Invoke((MethodInvoker)(() => listBox1.Items.Add("CurrentTExtendedOn Parse Hatası")));
        }

        public void CurrentTExtendedSet(string current)
        {
            try
            {
                SerialWrite(serialPort2, "PACE:CURR3 " + current + "\n");
                //string receivedData = ReadSerial();
                Invoke((MethodInvoker)(() => listBox1.Items.Add("PACE:CURR3 :" + current + "A")));
                //listBox1.Items.Add(receivedData);
                //var result = ConvertExpoToDec(receivedData);
                //if (!result) Invoke((MethodInvoker)(() => listBox1.Items.Add("ConvertExpoToDec Parse Hatası")));
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => listBox1.Items.Add("CurrentSExtendedSet Hata: " + ex.Message)));
            }
        }


        public void RsComClose()
        {
            serialPort2.Close();
        }
        

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        private void DaylightTestingButton_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("THE DAYLIGHT TESTING STARTING");
            try
            {
                BasicOpticalSetting();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"baglantıları kontrol ediniz;)\n Hata : {ex.Message}", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            listBox1.Items.Add("Optik iletisim acik : " + serialPort1.IsOpen);
            listBox1.Items.Add("Baudrate : " + serialPort1.BaudRate + " secildi");
            listBox1.Items.Add(serialPort1.PortName);
            SerialClose(serialPort1);
        }

        public void StartTimer()
        {
            saat = 0;
            dakika = 0;
            saniye = 0;
            timer1.Enabled = true;
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            saniye++;
            if (saniye == 60)
            {
                saniye = 0;
                dakika++;
            }


            if (dakika == 60)
            {
                dakika = 0;
                saat++;
            }


            LblSecond.Text = saniye.ToString("00");
            LblMinute.Text = dakika.ToString("00");
            LblHour.Text = saat.ToString("00");
        }

        private void LblHour_Click(object sender, EventArgs e)
        {

        }

        
    }
}