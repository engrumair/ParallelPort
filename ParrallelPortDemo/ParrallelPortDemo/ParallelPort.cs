using System;
using System.Collections.Generic;
using System.Text;

namespace ParrallelPortDemo
{
    /// <summary>
    /// Parallel port cofiguration, input and output handling class
    /// </summary>
    public class ParallelPort
    {


        // Parralel port registers and variables
                ushort parallelBaseAddress;
                ushort parallelEcrRegister;
                ushort parallelControllRegister;
                ushort parallelDataRegister;
                ushort parallelStatusRegister;
                ushort parallelConfigRegisterA;
                ushort parallelConfigRegisterB;

                ushort parallelEcpDataFifo;
                byte fifoFull;
                byte fifoEmpty; 
                ushort   sppMode           ;
                ushort  byteMode          ;
                ushort  parallelFifoMode      ;
                ushort  ecpMode            ;
                ushort  eppMode            ;
                ushort  reservedMode      ;
                ushort  fifoTestMode      ;
                ushort  configurationMode ;

               

                List<byte> expandableBuffer = new List<byte>();
                byte[] smallBuffer = new byte[16];


                public delegate void EcpDataReceivedHandler();

        /// <summary>
        /// Register to this event which will be raised when approximately 6000 bytes are added to the buffer
        /// </summary>
                public event EcpDataReceivedHandler DataReceived;

      
        /// <summary>
        /// Open ECP port
        /// </summary>
        public void OpenEcpPort()
        {
            PPort.OpenTVicPort();
            InitializeParallelPort(0x378);
            SetParEcpMode();
            DirPeripheralToPc();
            



        }
        /// <summary>
        /// Constructor
        /// </summary>
        public ParallelPort()
        {
            
        }

        /// <summary>
        /// It will return buffer which is expandable
        /// </summary>
        /// <returns>List of data bytes</returns>
        public List<byte> GetEcpBufferEx()
        {
            return this.expandableBuffer;
        }

        /// <summary>
        /// It will start receiving data and raise event as soon as 6000 bytes are added to buffer
        /// </summary>
        public  byte  ReceiveData()
        {

         return (   PPort.ReadPort(0x378 + 0x400) );

            
                     
         }

        public void WriteData(byte aValue)
        {

            PPort.WritePort(0x378 + 0x400, aValue);
        
        }
          /// <summary>
          /// Set parralel port in ECP mode
          /// </summary>
        public  void SetParEcpMode()
            {
            byte PortData;
            PortData = PPort.ReadPort(parallelEcrRegister);
            PortData = (byte)(   (((int)PortData) & (0x1F)) | (((int)ecpMode) << 5)  );
            PPort.WritePort(parallelEcrRegister,PortData);
            }
        /// <summary>
        /// Set parralel port in SPP mode
        /// </summary>
        public void  SetParSppMode()
        {
        byte PortData;
        PortData = PPort.ReadPort(parallelEcrRegister);
        PortData = (byte)((((int)PortData) & (0x1F)) | (((int)sppMode) << 5));
        PPort.WritePort(parallelEcrRegister,PortData);

        }

        /// <summary>
        /// Test ,if Fifo Fill, by checking the second bit of ECR register
        /// </summary>
        /// <returns>Return byte which contain the data byte of ECR register</returns>
        public byte  TestFifoFull()
        {
            byte PortData;
            PortData = PPort.ReadPort(parallelEcrRegister);
        return (byte) (PortData & fifoFull);
        }
        
        /// <summary>
        /// Test, if Fifo empty, by checking the first bit of ECR register.
        /// </summary>
        /// <returns>Return byte which contain the data byte of ECR register</returns>
        public byte  TestFifoEmpty()
        {
            byte PortData;
            PortData = PPort.ReadPort(parallelEcrRegister);
            return (byte)(PortData & fifoEmpty);

        }

        /// <summary>
        /// Perform closing step for closing parralel port in ECP mode
        /// </summary>
        public void CloseEcpPort()
        {

            DirPcToPeripheral();
            SetParSppMode();

            PPort.CloseTVicPort();

        }


        /// <summary>
        /// Write the given byte to control port
        /// </summary>
        /// <param name="PortData">Byte data to write to control port</param>
        public void WriteControllPort(byte PortData)
        {
        PortData = (byte)(PortData  ^ 0x0B);
        PPort.WritePort(0x378+2,PortData);
        }
        /// <summary>
        /// Reading Control port data.
        /// </summary>
        /// <returns>Return byte stored in control port</returns>
       public byte  ReadControllPort()
        {
        byte PortData;
        PortData = PPort.ReadPort(0x378+2);
        PortData = (byte)(PortData ^0x0B);
        return PortData;
        }

        /// <summary>
        /// Set a bit at specific bit position
        /// </summary>
        /// <param name="Byte">Byte Data to operate on</param>
        /// <param name="BitNo">Bit number to set in the given byte</param>
        /// <returns></returns>
        public byte SetBit(byte Byte,byte BitNo)
        {
        Byte =(byte) (Byte | (1<< BitNo));
        return Byte;
        }
        /// <summary>
        /// clear a bit at specific bit position
        /// </summary>
        /// <param name="Byte">Byte Data to operate on</param>
        /// <param name="BitNo">Bit number to clear in the given byte</param>
        /// <returns></returns>
        public byte  ClearBit(byte Byte,byte BitNo)
        {
        Byte = (byte)(Byte & ~( 1 << BitNo));
        return Byte;
        }
           
      

      /// <summary>
      /// Setting up a control port
      /// </summary>
      /// <param name="BitNo">Bit position</param>
      /// <returns>Byte data which this method write on the control port</returns>
        public byte SetControllPortBit(byte BitNo)
        {
        byte PortData;
        PortData = ReadControllPort();
        PortData = SetBit(PortData,BitNo);
        WriteControllPort(PortData);
        return PortData;
        }
        /// <summary>
        /// Clear specific bit at the control port
        /// </summary>
        /// <param name="BitNo">Bit position to clear</param>
        /// <returns>Byte data which this method write on the control port</returns>
        public byte ClearControllPortBit(byte BitNo)
        {
        byte PortData;
        PortData = ReadControllPort();
        PortData = ClearBit(PortData,BitNo);
        WriteControllPort(PortData);
        return PortData;

        }
        /// <summary>
        /// Settig mode of the peripheral
        /// </summary>
        public void DirPeripheralToPc()
        {
        byte PortData;
        PortData = 0;
        PortData = SetControllPortBit(0);
        PortData = SetControllPortBit(3);
        PortData = SetControllPortBit(1);
        ClearControllPortBit(2);
        PortData = SetControllPortBit(5);
        }
        /// <summary>
        /// Setting mode of the peripheral
        /// </summary>
        public void DirPcToPeripheral()
        {
        byte PortData;
        PortData = 0;
        PortData = SetControllPortBit(0);
        PortData = SetControllPortBit(3);
        PortData = ClearControllPortBit(1);
        PortData = SetControllPortBit(2);
        PortData = ClearControllPortBit(0);

        }

        /// <summary>
        /// Serial Port initialization
        /// </summary>
        /// <param name="BaseAddress">Base address of parallel port. It is usually 0x378 </param>
        public void InitializeParallelPort(ushort  BaseAddress)
        {
        parallelBaseAddress         =       BaseAddress;
        parallelControllRegister = (ushort)(parallelBaseAddress + 0x2);
        parallelStatusRegister = (ushort)(parallelBaseAddress + 0x1);
        parallelEcrRegister = (ushort)(parallelBaseAddress + 0x402);
        parallelEcpDataFifo = (ushort)(parallelBaseAddress + 0x400);
        parallelConfigRegisterA = (ushort)(parallelBaseAddress + 0x400);
        parallelConfigRegisterB = (ushort)(parallelBaseAddress + 0x400);

          sppMode           = 0x00;
          byteMode          = 0x01;
          parallelFifoMode       = 0x02;
          ecpMode           = 0x03 ;
          eppMode           = 0x04 ;
          reservedMode      = 0x05;
          fifoTestMode      = 0x06;
          configurationMode = 0x07;


            
            fifoEmpty = 0x01;
            fifoFull = 0x02;

        }





    }
}
