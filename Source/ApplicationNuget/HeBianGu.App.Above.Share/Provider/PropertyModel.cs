using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.App.Above
{
    public class PropertyListModel
    {
        [Display(Name = "类型Ints")]
        public int[] Int { get; set; }

        [Display(Name = "类型UInts")]
        public uint[] UInt { get; set; }

        [Display(Name = "类型Shorts")]
        public short[] Short { get; set; }

        [Display(Name = "类型UShorts")]
        public ushort[] UShort { get; set; }

        [Display(Name = "类型Longs")]
        public long[] Long { get; set; }

        [Display(Name = "类型ULongs")]
        public ulong[] ULong { get; set; }

        [Display(Name = "类型Bytes")]
        public byte[] Byte { get; set; }

        [Display(Name = "类型Double")]
        public double[] Double { get; set; }

        [Display(Name = "类型Floats")]
        public float[] Float { get; set; }

        [Display(Name = "类型Strings")]
        public string[] String { get; set; }

        [Display(Name = "类型DateTimes")]
        public DateTime[] DateTimes { get; set; }

        [Display(Name = "类型BoolS")]
        public bool[] BoolS { get; set; }

        [Display(Name = "ModelArray")]
        public PropertyModel[] ModelArray { get; set; }

        [Display(Name = "ModelList")]
        public List<PropertyModel> ModelList { get; set; }

    }

    public class PropertyModel
    {
        [Display(Name = "类型Int")]
        public int Ints { get; set; }

        [Display(Name = "类型UInt")]
        public uint UInt { get; set; }

        [Display(Name = "类型Short")]
        public short Short { get; set; }

        [Display(Name = "类型UShort")]
        public ushort UShort { get; set; }

        [Display(Name = "类型Long")]
        public long Long { get; set; }

        [Display(Name = "类型ULong")]
        public ulong ULong { get; set; }

        [Display(Name = "类型Byte")]
        public byte Byte { get; set; }

        [Display(Name = "类型Double")]
        public double Double { get; set; }

        [Display(Name = "类型Float")]
        public float Float { get; set; }


        [Display(Name = "类型String")]
        public string String { get; set; }


        [Display(Name = "类型DateTime")]
        public DateTime DateTime { get; set; }

        [Display(Name = "类型Bool")]
        public bool Bool { get; set; }

        [Display(Name = "类型Char")]
        public char Char { get; set; }

        [Display(Name = "类型Sbyte")]
        public sbyte Sbyte { get; set; }
    }
}
