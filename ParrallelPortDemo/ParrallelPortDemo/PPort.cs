using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;


namespace ParrallelPortDemo
{
    /// <summary>
    /// Wrapper class around TVicPort.dll Library
    /// </summary>
   public class PPort
    {

        [DllImport("TVicPort.dll", EntryPoint = "OpenTVicPort", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint OpenTVicPort();
        [DllImport("TVicPort.dll", EntryPoint = "CloseTVicPort", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void CloseTVicPort();
        [DllImport("TVicPort.dll", EntryPoint = "IsDriverOpened", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint IsDriverOpened();
        //----------------------------------------------------------------------------
        [DllImport("TVicPort.dll", EntryPoint = "ReadPort", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern byte ReadPort(ushort PortAddr);
        [DllImport("TVicPort.dll", EntryPoint = "ReadPortW", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern UInt16 ReadPortW(ushort PortAddr);
        [DllImport("TVicPort.dll", EntryPoint = "ReadPortL", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint ReadPortL(ushort PortAddr);
        [DllImport("TVicPort.dll", EntryPoint = "WritePort", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void WritePort(ushort PortAddr, byte bValue);
        [DllImport("TVicPort.dll", EntryPoint = "WritePortW", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void WritePortW(ushort PortAddr, UInt16 wValue);
        [DllImport("TVicPort.dll", EntryPoint = "WritePortL", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void WritePortL(ushort PortAddr, uint lValue);
        [DllImport("TVicPort.dll", EntryPoint = "SetHardAccess", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void SetHardAccess(uint newstate);
        [DllImport("TVicPort.dll", EntryPoint = "TestHardAccess", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint SetHardAccess();
       
        //----------------------------------------------------------------------------
        [DllImport("TVicPort.dll", EntryPoint = "GetLPTBasePort", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern ushort GetLPTBasePort();
        [DllImport("TVicPort.dll", EntryPoint = "GetLPTNumPorts", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern byte GetLPTNumPorts();
        [DllImport("TVicPort.dll", EntryPoint = "SetLPTNumber", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void SetLPTNumber(ushort LptN);
        [DllImport("TVicPort.dll", EntryPoint = "GetLPTNumber", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern ushort GetLPTNumber();
        [DllImport("TVicPort.dll", EntryPoint = "GetPin", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint GetPin(byte nPin);
        [DllImport("TVicPort.dll", EntryPoint = "SetPin", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void SetPin(byte nPin, uint State);
        //----------------------------------------------------------------------------
        [DllImport("TVicPort.dll", EntryPoint = "GetLPTAckwl", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint GetLPTAckwl();
        [DllImport("TVicPort.dll", EntryPoint = "GetLPTBusy", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint GetLPTBusy();
        [DllImport("TVicPort.dll", EntryPoint = "GetLPTError", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint GetLPTError();
        [DllImport("TVicPort.dll", EntryPoint = "GetLPTPaperEnd", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint GetLPTPaperEnd();
        [DllImport("TVicPort.dll", EntryPoint = "GetLPTSlct", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint GetLPTSlct();
       
    }
}
