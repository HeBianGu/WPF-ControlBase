using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace HeBianGu.Systems.WinTool
{
    public class PerforemanceCounterService : LazyInstance<PerforemanceCounterService>
    {
        /// <summary>
        /// “Cache”（缓存）、“Memory”（内存）、“Objects”（对象）、“PhysicalDisk”（物理磁盘）、“Process”（进程）、“Processor”（处理器）、“Server”（服务器）、“System”（系统）和“Thread”（线程）
        //SMB Server Shares
        //MSSQL$SQLEXPRESS:Wait Statistics
        //Hyper-V Hypervisor Partition
        //Hyper-V Dynamic Memory VM
        //Fax Service
        //WinNAT TCP
        //Hyper-V Virtual Storage Device
        //SQLAgent$SQLEXPRESS:SystemJobs
        //IPsec Connections
        //Generic IKEv1, AuthIP, and IKEv2
        //Pacer Pipe
        //Hyper-V Virtual Network Adapter
        //Storage Spaces Write Cache
        //SMSvcHost 3.0.0.0
        //UDPv6
        //Energy Meter
        //Event Tracing for Windows
        //MSSQL$SQLEXPRESS:User Settable
        //Hyper-V Configuration
        //TCPv4
        //RAS
        //Per Processor Network Interface Card Activity
        //XHCI CommonBuffer
        //MSSQL$SQLEXPRESS: Advanced Analytics
        //PacketDirect EC Utilization
        //MSSQL$SQLEXPRESS:SQL Errors
        //ServiceModelEndpoint 3.0.0.0
        //MSSQL$SQLEXPRESS:Broker Statistics
        //ReadyBoost Cache
        //IPv6
        //Pacer Flow
        //Network QoS Policy
        //Teredo Server
        //Hyper-V Virtual Machine Bus
        //Thermal Zone Information
        //Hyper-V Hypervisor Virtual Processor
        //MSSQL$SQLEXPRESS:Cursor Manager Total
        //.NET CLR Jit
        //Microsoft Winsock BSP
        //MSSQL$SQLEXPRESS:LogPool FreePool
        //MSSQL$SQLEXPRESS:FileTable
        //MSSQL$SQLEXPRESS:Cursor Manager by Type
        //SMSvcHost 4.0.0.0
        //MSSQL$SQLEXPRESS:Database Replica
        //RAS Total
        //NUMA Node Memory
        //SQLAgent$SQLEXPRESS:Jobs
        //MSSQL$SQLEXPRESS: 外部脚本
        //IPsec AuthIP IPv6
        //ASP.NET v4.0.30319
        //.NET CLR LocksAndThreads
        //MSSQL$SQLEXPRESS:Replication Merge
        //MSSQL$SQLEXPRESS:Replication Agents
        //Hyper-V Virtual Switch Port
        //Bluetooth Device
        //Processor
        //Hyper-V VM IO APIC
        //MSSQL$SQLEXPRESS:Access Methods
        //Synchronization
        //PacketDirect Receive Filters
        //Storage QoS Filter - Flow
        //SMB Server Sessions
        //Authorization Manager Applications
        //MSSQL$SQLEXPRESS:Exec Statistics
        //.NET CLR Exceptions
        //MSSQL$SQLEXPRESS:Memory Broker Clerks
        //.NET CLR Loading
        //Hyper-V VM Save, Snapshot, and Restore
        //ReFS
        //Hyper-V VM Vid Driver
        //Database ==> TableClasses
        //RAS Port
        //MSSQL$SQLEXPRESS:Memory Manager
        //IPsec IKEv1 IPv4
        //System
        //Hyper-V Virtual IDE Controller (Emulated)
        //Hyper-V Hypervisor Root Virtual Processor
        //IPsec IKEv2 IPv4
        //MSSQL$SQLEXPRESS:Replication Dist.
        //TCPIP Performance Diagnostics (Per-CPU)
        //ASP.NET Applications
        //SynchronizationNuma
        //MSSQL$SQLEXPRESS:Database Mirroring
        //Hyper-V VM Virtual Device Pipe IO
        //Teredo Relay
        //MSSQL$SQLEXPRESS:Workload Group Stats
        //.NET CLR Networking 4.0.0.0
        //Processor Information
        //SQLAgent$SQLEXPRESS:JobSteps
        //Hyper-V VM Vid Numa Node
        //Terminal Services
        //IPsec Driver
        //WSMan Quota Statistics
        //Network Interface
        //MSSQL$SQLEXPRESS:Columnstore
        //.NET CLR Memory
        //MSSQL$SQLEXPRESS:Plan Cache
        //Thread
        //MSSQL$SQLEXPRESS:CLR
        //Paging File
        //WinNAT UDP
        //Distributed Routing Table
        //MSSQL$SQLEXPRESS:Latches
        //PowerShell Workflow
        //BranchCache
        //RemoteFX Root GPU Management
        //Offline Files
        //WF(System.Workflow) 4.0.0.0
        //Hyper-V Hypervisor Root Partition
        //Teredo Client
        //Hyper-V Virtual Network Adapter Drop Reasons
        //LogicalDisk
        //ServiceModelOperation 4.0.0.0
        //IPv4
        //MSSQL$SQLEXPRESS:Locks
        //MSSQL$SQLEXPRESS:Broker/DBM Transport
        //MSSQL$SQLEXPRESS:Replication Snapshot
        //Storage Spaces Virtual Disk
        //MSSQL$SQLEXPRESS:General Statistics
        //IPsec IKEv2 IPv6
        //WinNAT
        //RemoteFX Graphics
        //Event Tracing for Windows Session
        //MSSQL$SQLEXPRESS:Databases
        //Storage Spaces Drt
        //Peer Name Resolution Protocol
        //PacketDirect Receive Counters
        //MSSQL$SQLEXPRESS:Availability Group
        //Hyper-V Virtual Switch Processor
        //PacketDirect Transmit Counters
        //ServiceModelService 4.0.0.0
        //Hyper-V Dynamic Memory Integration Service
        //WinNAT Instance
        //.NET CLR Interop
        //ServiceModelService 3.0.0.0
        //Hyper-V Virtual Machine Health Summary
        //Hyper-V Hypervisor
        //MSSQL$SQLEXPRESS:Batch Resp Statistics
        //TCPIP Performance Diagnostics
        //MSSQL$SQLEXPRESS:HTTP Storage
        //Browser
        //Database
        //IPHTTPS Session
        //UDPv4
        //Memory
        //Telephony
        //WFP Reauthorization
        //Hyper-V Hypervisor Logical Processor
        //Netlogon
        //NBT Connection
        //.NET Data Provider for Oracle
        //SMB Server
        //Per Processor Network Activity Cycles
        //DNS64 Global
        //IPsec IKEv1 IPv6
        //MSSQL$SQLEXPRESS:Broker Activation
        //Bluetooth Radio
        //Hyper-V VM Live Migration
        //WinNAT ICMP
        //MSSQL$SQLEXPRESS:Memory Node
        //Event Log
        //Search Indexer
        //.NET CLR Remoting
        //Cache
        //Hyper-V Dynamic Memory Balancer
        //ICMPv6
        //MSSQL$SQLEXPRESS:Query Store
        //MSSQL$SQLEXPRESS:Buffer Node
        //Process
        //Windows Workflow Foundation
        //SQLAgent$SQLEXPRESS:Alerts
        //Storage QoS Filter - Volume
        //ServiceModelOperation 3.0.0.0
        //Hyper-V Worker Virtual Processor
        //Job Object Details
        //MSDTC Bridge 4.0.0.0
        //ASP.NET Apps v4.0.30319
        //.NET Memory Cache 4.0
        //MSSQL$SQLEXPRESS:Resource Pool Stats
        //SQLAgent$SQLEXPRESS:Statistics
        //Database ==> Instances
        //Power Meter
        //ICMP
        //No name
        //Network Adapter
        //BitLocker
        //ServiceModelEndpoint 4.0.0.0
        //SMB Direct Connection
        //PacketDirect Queue Depth
        //ASP.NET
        //MSSQL$SQLEXPRESS:Buffer Manager
        //PhysicalDisk
        //Objects
        //IPsec AuthIP IPv4
        //.NET CLR Security
        //Print Queue
        //MSSQL$SQLEXPRESS:Availability Replica
        //MSSQL$SQLEXPRESS:Replication Logreader
        //TCPv6
        //Windows Time Service
        //Redirector
        //Hyper-V VM Remoting
        //WFPv6
        //Hyper-V VM Vid Partition
        //Storage Management WSP Spaces Runtime
        //Hyper-V Virtual Switch
        //Client Side Caching
        //ASP.NET State Service
        //.NET CLR Data
        //WorkflowServiceHost 4.0.0.0
        //RemoteFX Network
        //.NET Data Provider for SqlServer
        //Physical Network Interface Card Activity
        //IPHTTPS Global
        //XHCI TransferRing
        //MSSQL$SQLEXPRESS:Catalog Metadata
        //.NET CLR Networking
        //MSSQL$SQLEXPRESS:SQL Statistics
        //Hyper-V VM Worker Process Memory Manager
        //Hyper-V Virtual Network Adapter VRSS
        //Hyper-V Legacy Network Adapter
        //WFP
        //MSSQL$SQLEXPRESS:Transactions
        //MSSQL$SQLEXPRESS:Deprecated Features
        //FileSystem Disk Activity
        //Database ==> Databases
        //Storage Spaces Tier
        //MSDTC Bridge 3.0.0.0
        //HTTP Service
        //MSSQL$SQLEXPRESS:Broker TO Statistics
        //MSSQL$SQLEXPRESS:Backup Device
        //WFPv4
        //Hyper-V Virtual SMB
        //WFP Classify
        //XHCI Interrupter
        //Hyper-V Replica VM
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetCategoryNames()
        {
            PerformanceCounterCategory[] pcc = PerformanceCounterCategory.GetCategories();
            return pcc.Select(x => x.CategoryName);
        }
        /// <summary>
        /// 
        //        2
        //4
        //3
        //7
        //_Total
        //1
        //0
        //6
        //5
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public IEnumerable<string> GetInstanceNames(string categoryName)
        {
            PerformanceCounterCategory diskCounter = new PerformanceCounterCategory(categoryName);
            return diskCounter.GetInstanceNames();
        }

        /// <summary>
        /// Current Disk Queue Length
        //% Disk Time
        //Avg.Disk Queue Length
        //% Disk Read Time
        //Avg.Disk Read Queue Length
        //% Disk Write Time
        //Avg.Disk Write Queue Length
        //Avg.Disk sec/ Transfer
        //Avg.Disk sec/ Read
        //Avg.Disk sec/ Write
        //Disk Transfers/ sec
        //Disk Reads/ sec
        //Disk Writes/ sec
        //Disk Bytes/ sec
        //Disk Read Bytes / sec
        //Disk Write Bytes / sec
        //Avg.Disk Bytes/ Transfer
        //Avg.Disk Bytes/ Read
        //Avg.Disk Bytes/ Write
        //% Idle Time
        //Split IO / Sec
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="instaneName"></param>
        /// <returns></returns>
        public IEnumerable<string> GetCounterNames(string categoryName, string instaneName)
        {
            PerformanceCounterCategory diskCounter = new PerformanceCounterCategory(categoryName);
            PerformanceCounter[] counters = diskCounter.GetCounters(instaneName);
            return counters.Select(x => x.CategoryName);
        }

        public IEnumerable<T> GetCounterPresenters<T>(string categoryName, string instaneName, Func<string, string, string, T> creator) where T : ICounterPresenter
        {
            PerformanceCounterCategory diskCounter = new PerformanceCounterCategory(categoryName);
            PerformanceCounter[] counters = diskCounter.GetCounters(instaneName);
            return counters.Select(x => creator.Invoke(categoryName, instaneName, x.CounterName));
        }


        public IEnumerable<T> GetCounterPresenters<T>(string categoryName, Func<string, string, T> creator) where T : ICounterPresenter
        {
            PerformanceCounterCategory diskCounter = new PerformanceCounterCategory(categoryName);
            PerformanceCounter[] counters = diskCounter.GetCounters();
            return counters.Select(x => creator.Invoke(categoryName, x.CounterName));
        }

        public IEnumerable<T> GetCounterPresenters<T>(string categoryName, Func<string, string, string, T> creator) where T : ICounterPresenter
        {
            if (PerformanceCounterCategory.Exists(categoryName))
            {
                PerformanceCounterCategory diskCounter = new PerformanceCounterCategory(categoryName);
                var instances = diskCounter.GetInstanceNames();
                foreach (var instaneName in instances)
                {
                    if (!PerformanceCounterCategory.InstanceExists(instaneName, diskCounter.CategoryName))
                        continue;
                    PerformanceCounter[] counters = diskCounter.GetCounters(instaneName);
                    foreach (var x in counters)
                    {
                        if (!PerformanceCounterCategory.CounterExists(x.CounterName, diskCounter.CategoryName))
                            continue;
                        yield return creator.Invoke(diskCounter.CategoryName, instaneName, x.CounterName);
                    }
                }
            }
        }
    }
}
