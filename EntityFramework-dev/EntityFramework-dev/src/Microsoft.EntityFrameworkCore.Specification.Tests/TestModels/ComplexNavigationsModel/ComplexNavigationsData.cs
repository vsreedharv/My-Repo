﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.EntityFrameworkCore.Specification.Tests.TestModels.ComplexNavigationsModel
{
    public static class ComplexNavigationsData
    {
        private static readonly Level1[] _levelOnes;
        private static readonly Level2[] _levelTwos;
        private static readonly Level3[] _levelThrees;
        private static readonly Level4[] _levelFours;

        static ComplexNavigationsData()
        {
            _levelOnes = CreateLevelOnes();
            _levelTwos = CreateLevelTwos();
            _levelThrees = CreateLevelThrees();
            _levelFours = CreateLevelFours();

            foreach (var l1 in _levelOnes)
            {
                l1.OneToMany_Optional = new List<Level2>();
                l1.OneToMany_Optional_Self = new List<Level1>();
                l1.OneToMany_Required = new List<Level2>();
                l1.OneToMany_Required_Self = new List<Level1>();
            }

            foreach (var l2 in _levelTwos)
            {
                l2.OneToMany_Optional = new List<Level3>();
                l2.OneToMany_Optional_Self = new List<Level2>();
                l2.OneToMany_Required = new List<Level3>();
                l2.OneToMany_Required_Self = new List<Level2>();
            }

            foreach (var l3 in _levelThrees)
            {
                l3.OneToMany_Optional = new List<Level4>();
                l3.OneToMany_Optional_Self = new List<Level3>();
                l3.OneToMany_Required = new List<Level4>();
                l3.OneToMany_Required_Self = new List<Level3>();
            }

            foreach (var l4 in _levelFours)
            {
                l4.OneToMany_Optional_Self = new List<Level4>();
                l4.OneToMany_Required_Self = new List<Level4>();
            }

            WireUpPart1(_levelOnes, _levelTwos, _levelThrees, _levelFours);
            WireUpInversePart1(_levelOnes, _levelTwos, _levelThrees, _levelFours);

            WireUpPart2(_levelOnes, _levelTwos, _levelThrees, _levelFours);
            WireUpInversePart2(_levelOnes, _levelTwos, _levelThrees, _levelFours);
        }

        public static Level1[] CreateLevelOnes() =>
            new[]
            {
                new Level1 { Id = 1, Name = "L1 01", Date = new DateTime(2001, 1, 1) },
                new Level1 { Id = 2, Name = "L1 02", Date = new DateTime(2002, 2, 2) },
                new Level1 { Id = 3, Name = "L1 03", Date = new DateTime(2003, 3, 3) },
                new Level1 { Id = 4, Name = "L1 04", Date = new DateTime(2004, 4, 4) },
                new Level1 { Id = 5, Name = "L1 05", Date = new DateTime(2005, 5, 5) },
                new Level1 { Id = 6, Name = "L1 06", Date = new DateTime(2006, 6, 6) },
                new Level1 { Id = 7, Name = "L1 07", Date = new DateTime(2007, 7, 7) },
                new Level1 { Id = 8, Name = "L1 08", Date = new DateTime(2008, 8, 8) },
                new Level1 { Id = 9, Name = "L1 09", Date = new DateTime(2009, 9, 9) },
                new Level1 { Id = 10, Name = "L1 10", Date = new DateTime(2010, 10, 10) },
                new Level1 { Id = 11, Name = "L1 11", Date = new DateTime(2009, 11, 11) },
                new Level1 { Id = 12, Name = "L1 12", Date = new DateTime(2008, 12, 12) },
                new Level1 { Id = 13, Name = "L1 13", Date = new DateTime(2007, 1, 1) },
            };

        public static Level2[] CreateLevelTwos() =>
            new[]
            {
                new Level2 { Id = 1, Name = "L2 01", Date = new DateTime(2010, 10, 10) },
                new Level2 { Id = 2, Name = "L2 02", Date = new DateTime(2002, 2, 2) },
                new Level2 { Id = 3, Name = "L2 03", Date = new DateTime(2008, 8, 8) },
                new Level2 { Id = 4, Name = "L2 04", Date = new DateTime(2004, 4, 4) },
                new Level2 { Id = 5, Name = "L2 05", Date = new DateTime(2006, 6, 6) },
                new Level2 { Id = 6, Name = "L2 06", Date = new DateTime(2005, 5, 5) },
                new Level2 { Id = 7, Name = "L2 07", Date = new DateTime(2007, 7, 7) },
                new Level2 { Id = 8, Name = "L2 08", Date = new DateTime(2003, 3, 3) },
                new Level2 { Id = 9, Name = "L2 09", Date = new DateTime(2009, 9, 9) },
                new Level2 { Id = 10, Name = "L2 10", Date = new DateTime(2001, 1, 1) },
                new Level2 { Id = 11, Name = "L2 11", Date = new DateTime(2000, 1, 1) },
            };

        public static Level3[] CreateLevelThrees() =>
            new[]
            {
                new Level3 { Id = 1, Name = "L3 01" },
                new Level3 { Id = 2, Name = "L3 02" },
                new Level3 { Id = 3, Name = "L3 03" },
                new Level3 { Id = 4, Name = "L3 04" },
                new Level3 { Id = 5, Name = "L3 05" },
                new Level3 { Id = 6, Name = "L3 06" },
                new Level3 { Id = 7, Name = "L3 07" },
                new Level3 { Id = 8, Name = "L3 08" },
                new Level3 { Id = 9, Name = "L3 09" },
                new Level3 { Id = 10, Name = "L3 10" },
            };

        public static Level4[] CreateLevelFours() =>
            new[]
            {
                new Level4 { Id = 1, Name = "L4 01" },
                new Level4 { Id = 2, Name = "L4 02" },
                new Level4 { Id = 3, Name = "L4 03" },
                new Level4 { Id = 4, Name = "L4 04" },
                new Level4 { Id = 5, Name = "L4 05" },
                new Level4 { Id = 6, Name = "L4 06" },
                new Level4 { Id = 7, Name = "L4 07" },
                new Level4 { Id = 8, Name = "L4 08" },
                new Level4 { Id = 9, Name = "L4 09" },
                new Level4 { Id = 10, Name = "L4 10" },
            };

        public static void WireUpPart1(Level1[] l1s, Level2[] l2s, Level3[] l3s, Level4[] l4s)
        {
            l1s[0].OneToOne_Required_PK = l2s[0];
            l1s[1].OneToOne_Required_PK = l2s[1];
            l1s[2].OneToOne_Required_PK = l2s[2];
            l1s[3].OneToOne_Required_PK = l2s[3];
            l1s[4].OneToOne_Required_PK = l2s[4];
            l1s[5].OneToOne_Required_PK = l2s[5];
            l1s[6].OneToOne_Required_PK = l2s[6];
            l1s[7].OneToOne_Required_PK = l2s[7];
            l1s[8].OneToOne_Required_PK = l2s[8];
            l1s[9].OneToOne_Required_PK = l2s[9];
            l1s[10].OneToOne_Required_PK = l2s[10];

            l1s[0].OneToOne_Required_FK = l2s[9];
            l1s[1].OneToOne_Required_FK = l2s[8];
            l1s[2].OneToOne_Required_FK = l2s[7];
            l1s[3].OneToOne_Required_FK = l2s[6];
            l1s[4].OneToOne_Required_FK = l2s[5];
            l1s[5].OneToOne_Required_FK = l2s[4];
            l1s[6].OneToOne_Required_FK = l2s[3];
            l1s[7].OneToOne_Required_FK = l2s[2];
            l1s[8].OneToOne_Required_FK = l2s[1];
            l1s[9].OneToOne_Required_FK = l2s[0];
            l1s[10].OneToOne_Required_FK = l2s[10];

            l1s[0].OneToMany_Required = new List<Level2> { l2s[0], l2s[1], l2s[2], l2s[3], l2s[4], l2s[5], l2s[6], l2s[7], l2s[8], l2s[9], l2s[10] };

            l1s[0].OneToMany_Required_Self = new List<Level1> { l1s[0], l1s[1], l1s[11] };
            l1s[1].OneToMany_Required_Self = new List<Level1> { l1s[2], l1s[12] };
            l1s[2].OneToMany_Required_Self = new List<Level1> { l1s[3] };
            l1s[3].OneToMany_Required_Self = new List<Level1> { l1s[4] };
            l1s[4].OneToMany_Required_Self = new List<Level1> { l1s[5] };
            l1s[5].OneToMany_Required_Self = new List<Level1> { l1s[6] };
            l1s[6].OneToMany_Required_Self = new List<Level1> { l1s[7] };
            l1s[7].OneToMany_Required_Self = new List<Level1> { l1s[8] };
            l1s[8].OneToMany_Required_Self = new List<Level1> { l1s[9] };
            l1s[9].OneToMany_Required_Self = new List<Level1>();
            l1s[10].OneToMany_Required_Self = new List<Level1> { l1s[10] };
            l1s[11].OneToMany_Required_Self = new List<Level1>();
            l1s[12].OneToMany_Required_Self = new List<Level1>();

            l2s[0].OneToOne_Required_PK = l3s[0];
            l2s[1].OneToOne_Required_PK = l3s[1];
            l2s[2].OneToOne_Required_PK = l3s[2];
            l2s[3].OneToOne_Required_PK = l3s[3];
            l2s[4].OneToOne_Required_PK = l3s[4];
            l2s[5].OneToOne_Required_PK = l3s[5];
            l2s[6].OneToOne_Required_PK = l3s[6];
            l2s[7].OneToOne_Required_PK = l3s[7];
            l2s[8].OneToOne_Required_PK = l3s[8];
            l2s[9].OneToOne_Required_PK = l3s[9];

            l2s[0].OneToOne_Required_FK = l3s[9];
            l2s[1].OneToOne_Required_FK = l3s[8];
            l2s[2].OneToOne_Required_FK = l3s[7];
            l2s[3].OneToOne_Required_FK = l3s[6];
            l2s[4].OneToOne_Required_FK = l3s[5];
            l2s[5].OneToOne_Required_FK = l3s[4];
            l2s[6].OneToOne_Required_FK = l3s[3];
            l2s[7].OneToOne_Required_FK = l3s[2];
            l2s[8].OneToOne_Required_FK = l3s[1];
            l2s[9].OneToOne_Required_FK = l3s[0];

            l2s[0].OneToMany_Required = new List<Level3> { l3s[0], l3s[1], l3s[2], l3s[3], l3s[4], l3s[5], l3s[6], l3s[7], l3s[8], l3s[9] };

            l2s[0].OneToMany_Required_Self = new List<Level2> { l2s[0], l2s[1], l2s[10] };
            l2s[1].OneToMany_Required_Self = new List<Level2> { l2s[2] };
            l2s[2].OneToMany_Required_Self = new List<Level2> { l2s[3] };
            l2s[3].OneToMany_Required_Self = new List<Level2> { l2s[4] };
            l2s[4].OneToMany_Required_Self = new List<Level2> { l2s[5] };
            l2s[5].OneToMany_Required_Self = new List<Level2> { l2s[6] };
            l2s[6].OneToMany_Required_Self = new List<Level2> { l2s[7] };
            l2s[7].OneToMany_Required_Self = new List<Level2> { l2s[8] };
            l2s[8].OneToMany_Required_Self = new List<Level2> { l2s[9] };
            l2s[9].OneToMany_Required_Self = new List<Level2>();
            l2s[10].OneToMany_Required_Self = new List<Level2>();

            l3s[0].OneToOne_Required_PK = l4s[0];
            l3s[1].OneToOne_Required_PK = l4s[1];
            l3s[2].OneToOne_Required_PK = l4s[2];
            l3s[3].OneToOne_Required_PK = l4s[3];
            l3s[4].OneToOne_Required_PK = l4s[4];
            l3s[5].OneToOne_Required_PK = l4s[5];
            l3s[6].OneToOne_Required_PK = l4s[6];
            l3s[7].OneToOne_Required_PK = l4s[7];
            l3s[8].OneToOne_Required_PK = l4s[8];
            l3s[9].OneToOne_Required_PK = l4s[9];

            l3s[0].OneToOne_Required_FK = l4s[9];
            l3s[1].OneToOne_Required_FK = l4s[8];
            l3s[2].OneToOne_Required_FK = l4s[7];
            l3s[3].OneToOne_Required_FK = l4s[6];
            l3s[4].OneToOne_Required_FK = l4s[5];
            l3s[5].OneToOne_Required_FK = l4s[4];
            l3s[6].OneToOne_Required_FK = l4s[3];
            l3s[7].OneToOne_Required_FK = l4s[2];
            l3s[8].OneToOne_Required_FK = l4s[1];
            l3s[9].OneToOne_Required_FK = l4s[0];

            l3s[0].OneToMany_Required = new List<Level4> { l4s[0], l4s[1], l4s[2], l4s[3], l4s[4], l4s[5], l4s[6], l4s[7], l4s[8], l4s[9] };

            l3s[0].OneToMany_Required_Self = new List<Level3> { l3s[0], l3s[1] };
            l3s[1].OneToMany_Required_Self = new List<Level3> { l3s[2] };
            l3s[2].OneToMany_Required_Self = new List<Level3> { l3s[3] };
            l3s[3].OneToMany_Required_Self = new List<Level3> { l3s[4] };
            l3s[4].OneToMany_Required_Self = new List<Level3> { l3s[5] };
            l3s[5].OneToMany_Required_Self = new List<Level3> { l3s[6] };
            l3s[6].OneToMany_Required_Self = new List<Level3> { l3s[7] };
            l3s[7].OneToMany_Required_Self = new List<Level3> { l3s[8] };
            l3s[8].OneToMany_Required_Self = new List<Level3> { l3s[9] };
            l3s[9].OneToMany_Required_Self = new List<Level3>();

            l4s[0].OneToMany_Required_Self = new List<Level4> { l4s[0], l4s[1] };
            l4s[1].OneToMany_Required_Self = new List<Level4> { l4s[2] };
            l4s[2].OneToMany_Required_Self = new List<Level4> { l4s[3] };
            l4s[3].OneToMany_Required_Self = new List<Level4> { l4s[4] };
            l4s[4].OneToMany_Required_Self = new List<Level4> { l4s[5] };
            l4s[5].OneToMany_Required_Self = new List<Level4> { l4s[6] };
            l4s[6].OneToMany_Required_Self = new List<Level4> { l4s[7] };
            l4s[7].OneToMany_Required_Self = new List<Level4> { l4s[8] };
            l4s[8].OneToMany_Required_Self = new List<Level4> { l4s[9] };
            l4s[9].OneToMany_Required_Self = new List<Level4>();
        }

        public static void WireUpInversePart1(Level1[] l1s, Level2[] l2s, Level3[] l3s, Level4[] l4s)
        {
            l2s[0].OneToOne_Required_PK_Inverse = l1s[0];
            l2s[1].OneToOne_Required_PK_Inverse = l1s[1];
            l2s[2].OneToOne_Required_PK_Inverse = l1s[2];
            l2s[3].OneToOne_Required_PK_Inverse = l1s[3];
            l2s[4].OneToOne_Required_PK_Inverse = l1s[4];
            l2s[5].OneToOne_Required_PK_Inverse = l1s[5];
            l2s[6].OneToOne_Required_PK_Inverse = l1s[6];
            l2s[7].OneToOne_Required_PK_Inverse = l1s[7];
            l2s[8].OneToOne_Required_PK_Inverse = l1s[8];
            l2s[9].OneToOne_Required_PK_Inverse = l1s[9];
            l2s[10].OneToOne_Required_PK_Inverse = l1s[10];

            l2s[9].OneToOne_Required_FK_Inverse = l1s[0];
            l2s[8].OneToOne_Required_FK_Inverse = l1s[1];
            l2s[7].OneToOne_Required_FK_Inverse = l1s[2];
            l2s[6].OneToOne_Required_FK_Inverse = l1s[3];
            l2s[5].OneToOne_Required_FK_Inverse = l1s[4];
            l2s[4].OneToOne_Required_FK_Inverse = l1s[5];
            l2s[3].OneToOne_Required_FK_Inverse = l1s[6];
            l2s[2].OneToOne_Required_FK_Inverse = l1s[7];
            l2s[1].OneToOne_Required_FK_Inverse = l1s[8];
            l2s[0].OneToOne_Required_FK_Inverse = l1s[9];
            l2s[10].OneToOne_Required_FK_Inverse = l1s[10];

            l2s[9].Level1_Required_Id = l1s[0].Id;
            l2s[8].Level1_Required_Id = l1s[1].Id;
            l2s[7].Level1_Required_Id = l1s[2].Id;
            l2s[6].Level1_Required_Id = l1s[3].Id;
            l2s[5].Level1_Required_Id = l1s[4].Id;
            l2s[4].Level1_Required_Id = l1s[5].Id;
            l2s[3].Level1_Required_Id = l1s[6].Id;
            l2s[2].Level1_Required_Id = l1s[7].Id;
            l2s[1].Level1_Required_Id = l1s[8].Id;
            l2s[0].Level1_Required_Id = l1s[9].Id;
            l2s[10].Level1_Required_Id = l1s[10].Id;

            l2s[0].OneToMany_Required_Inverse = l1s[0];
            l2s[1].OneToMany_Required_Inverse = l1s[0];
            l2s[2].OneToMany_Required_Inverse = l1s[0];
            l2s[3].OneToMany_Required_Inverse = l1s[0];
            l2s[4].OneToMany_Required_Inverse = l1s[0];
            l2s[5].OneToMany_Required_Inverse = l1s[0];
            l2s[6].OneToMany_Required_Inverse = l1s[0];
            l2s[7].OneToMany_Required_Inverse = l1s[0];
            l2s[8].OneToMany_Required_Inverse = l1s[0];
            l2s[9].OneToMany_Required_Inverse = l1s[0];
            l2s[10].OneToMany_Required_Inverse = l1s[0];

            l1s[0].OneToMany_Required_Self_Inverse = l1s[0];
            l1s[1].OneToMany_Required_Self_Inverse = l1s[0];
            l1s[11].OneToMany_Required_Self_Inverse = l1s[0];
            l1s[2].OneToMany_Required_Self_Inverse = l1s[1];
            l1s[12].OneToMany_Required_Self_Inverse = l1s[1];
            l1s[3].OneToMany_Required_Self_Inverse = l1s[2];
            l1s[4].OneToMany_Required_Self_Inverse = l1s[3];
            l1s[5].OneToMany_Required_Self_Inverse = l1s[4];
            l1s[6].OneToMany_Required_Self_Inverse = l1s[5];
            l1s[7].OneToMany_Required_Self_Inverse = l1s[6];
            l1s[8].OneToMany_Required_Self_Inverse = l1s[7];
            l1s[9].OneToMany_Required_Self_Inverse = l1s[8];
            l1s[10].OneToMany_Required_Self_Inverse = l1s[10];

            l3s[0].OneToOne_Required_PK_Inverse = l2s[0];
            l3s[1].OneToOne_Required_PK_Inverse = l2s[1];
            l3s[2].OneToOne_Required_PK_Inverse = l2s[2];
            l3s[3].OneToOne_Required_PK_Inverse = l2s[3];
            l3s[4].OneToOne_Required_PK_Inverse = l2s[4];
            l3s[5].OneToOne_Required_PK_Inverse = l2s[5];
            l3s[6].OneToOne_Required_PK_Inverse = l2s[6];
            l3s[7].OneToOne_Required_PK_Inverse = l2s[7];
            l3s[8].OneToOne_Required_PK_Inverse = l2s[8];
            l3s[9].OneToOne_Required_PK_Inverse = l2s[9];

            l3s[9].OneToOne_Required_FK_Inverse = l2s[0];
            l3s[8].OneToOne_Required_FK_Inverse = l2s[1];
            l3s[7].OneToOne_Required_FK_Inverse = l2s[2];
            l3s[6].OneToOne_Required_FK_Inverse = l2s[3];
            l3s[5].OneToOne_Required_FK_Inverse = l2s[4];
            l3s[4].OneToOne_Required_FK_Inverse = l2s[5];
            l3s[3].OneToOne_Required_FK_Inverse = l2s[6];
            l3s[2].OneToOne_Required_FK_Inverse = l2s[7];
            l3s[1].OneToOne_Required_FK_Inverse = l2s[8];
            l3s[0].OneToOne_Required_FK_Inverse = l2s[9];

            l3s[9].Level2_Required_Id = l2s[0].Id;
            l3s[8].Level2_Required_Id = l2s[1].Id;
            l3s[7].Level2_Required_Id = l2s[2].Id;
            l3s[6].Level2_Required_Id = l2s[3].Id;
            l3s[5].Level2_Required_Id = l2s[4].Id;
            l3s[4].Level2_Required_Id = l2s[5].Id;
            l3s[3].Level2_Required_Id = l2s[6].Id;
            l3s[2].Level2_Required_Id = l2s[7].Id;
            l3s[1].Level2_Required_Id = l2s[8].Id;
            l3s[0].Level2_Required_Id = l2s[9].Id;

            l3s[0].OneToMany_Required_Inverse = l2s[0];
            l3s[1].OneToMany_Required_Inverse = l2s[0];
            l3s[2].OneToMany_Required_Inverse = l2s[0];
            l3s[3].OneToMany_Required_Inverse = l2s[0];
            l3s[4].OneToMany_Required_Inverse = l2s[0];
            l3s[5].OneToMany_Required_Inverse = l2s[0];
            l3s[6].OneToMany_Required_Inverse = l2s[0];
            l3s[7].OneToMany_Required_Inverse = l2s[0];
            l3s[8].OneToMany_Required_Inverse = l2s[0];
            l3s[9].OneToMany_Required_Inverse = l2s[0];

            l2s[0].OneToMany_Required_Self_Inverse = l2s[0];
            l2s[1].OneToMany_Required_Self_Inverse = l2s[0];
            l2s[10].OneToMany_Required_Self_Inverse = l2s[0];

            l2s[2].OneToMany_Required_Self_Inverse = l2s[1];
            l2s[3].OneToMany_Required_Self_Inverse = l2s[2];
            l2s[4].OneToMany_Required_Self_Inverse = l2s[3];
            l2s[5].OneToMany_Required_Self_Inverse = l2s[4];
            l2s[6].OneToMany_Required_Self_Inverse = l2s[5];
            l2s[7].OneToMany_Required_Self_Inverse = l2s[6];
            l2s[8].OneToMany_Required_Self_Inverse = l2s[7];
            l2s[9].OneToMany_Required_Self_Inverse = l2s[8];

            l4s[0].OneToOne_Required_PK_Inverse = l3s[0];
            l4s[1].OneToOne_Required_PK_Inverse = l3s[1];
            l4s[2].OneToOne_Required_PK_Inverse = l3s[2];
            l4s[3].OneToOne_Required_PK_Inverse = l3s[3];
            l4s[4].OneToOne_Required_PK_Inverse = l3s[4];
            l4s[5].OneToOne_Required_PK_Inverse = l3s[5];
            l4s[6].OneToOne_Required_PK_Inverse = l3s[6];
            l4s[7].OneToOne_Required_PK_Inverse = l3s[7];
            l4s[8].OneToOne_Required_PK_Inverse = l3s[8];
            l4s[9].OneToOne_Required_PK_Inverse = l3s[9];

            l4s[9].OneToOne_Required_FK_Inverse = l3s[0];
            l4s[8].OneToOne_Required_FK_Inverse = l3s[1];
            l4s[7].OneToOne_Required_FK_Inverse = l3s[2];
            l4s[6].OneToOne_Required_FK_Inverse = l3s[3];
            l4s[5].OneToOne_Required_FK_Inverse = l3s[4];
            l4s[4].OneToOne_Required_FK_Inverse = l3s[5];
            l4s[3].OneToOne_Required_FK_Inverse = l3s[6];
            l4s[2].OneToOne_Required_FK_Inverse = l3s[7];
            l4s[1].OneToOne_Required_FK_Inverse = l3s[8];
            l4s[0].OneToOne_Required_FK_Inverse = l3s[9];

            l4s[9].Level3_Required_Id = l3s[0].Id;
            l4s[8].Level3_Required_Id = l3s[1].Id;
            l4s[7].Level3_Required_Id = l3s[2].Id;
            l4s[6].Level3_Required_Id = l3s[3].Id;
            l4s[5].Level3_Required_Id = l3s[4].Id;
            l4s[4].Level3_Required_Id = l3s[5].Id;
            l4s[3].Level3_Required_Id = l3s[6].Id;
            l4s[2].Level3_Required_Id = l3s[7].Id;
            l4s[1].Level3_Required_Id = l3s[8].Id;
            l4s[0].Level3_Required_Id = l3s[9].Id;

            l4s[0].OneToMany_Required_Inverse = l3s[0];
            l4s[1].OneToMany_Required_Inverse = l3s[0];
            l4s[2].OneToMany_Required_Inverse = l3s[0];
            l4s[3].OneToMany_Required_Inverse = l3s[0];
            l4s[4].OneToMany_Required_Inverse = l3s[0];
            l4s[5].OneToMany_Required_Inverse = l3s[0];
            l4s[6].OneToMany_Required_Inverse = l3s[0];
            l4s[7].OneToMany_Required_Inverse = l3s[0];
            l4s[8].OneToMany_Required_Inverse = l3s[0];
            l4s[9].OneToMany_Required_Inverse = l3s[0];

            l3s[0].OneToMany_Required_Self_Inverse = l3s[0];
            l3s[1].OneToMany_Required_Self_Inverse = l3s[0];
            l3s[2].OneToMany_Required_Self_Inverse = l3s[1];
            l3s[3].OneToMany_Required_Self_Inverse = l3s[2];
            l3s[4].OneToMany_Required_Self_Inverse = l3s[3];
            l3s[5].OneToMany_Required_Self_Inverse = l3s[4];
            l3s[6].OneToMany_Required_Self_Inverse = l3s[5];
            l3s[7].OneToMany_Required_Self_Inverse = l3s[6];
            l3s[8].OneToMany_Required_Self_Inverse = l3s[7];
            l3s[9].OneToMany_Required_Self_Inverse = l3s[8];

            l4s[0].OneToMany_Required_Self_Inverse = l4s[0];
            l4s[1].OneToMany_Required_Self_Inverse = l4s[0];
            l4s[2].OneToMany_Required_Self_Inverse = l4s[1];
            l4s[3].OneToMany_Required_Self_Inverse = l4s[2];
            l4s[4].OneToMany_Required_Self_Inverse = l4s[3];
            l4s[5].OneToMany_Required_Self_Inverse = l4s[4];
            l4s[6].OneToMany_Required_Self_Inverse = l4s[5];
            l4s[7].OneToMany_Required_Self_Inverse = l4s[6];
            l4s[8].OneToMany_Required_Self_Inverse = l4s[7];
            l4s[9].OneToMany_Required_Self_Inverse = l4s[8];
        }

        public static void WireUpPart2(Level1[] l1s, Level2[] l2s, Level3[] l3s, Level4[] l4s)
        {
            l1s[0].OneToOne_Optional_PK = l2s[0];
            l1s[2].OneToOne_Optional_PK = l2s[2];
            l1s[4].OneToOne_Optional_PK = l2s[4];
            l1s[6].OneToOne_Optional_PK = l2s[6];
            l1s[8].OneToOne_Optional_PK = l2s[8];

            l1s[1].OneToOne_Optional_FK = l2s[8];
            l1s[3].OneToOne_Optional_FK = l2s[6];
            l1s[5].OneToOne_Optional_FK = l2s[4];
            l1s[7].OneToOne_Optional_FK = l2s[2];
            l1s[9].OneToOne_Optional_FK = l2s[0];

            l1s[0].OneToMany_Optional = new List<Level2> { l2s[1], l2s[3], l2s[5], l2s[7], l2s[9] };

            l1s[1].OneToMany_Optional_Self = new List<Level1> { l1s[0] };
            l1s[3].OneToMany_Optional_Self = new List<Level1> { l1s[2] };
            l1s[5].OneToMany_Optional_Self = new List<Level1> { l1s[4] };
            l1s[7].OneToMany_Optional_Self = new List<Level1> { l1s[6] };
            l1s[9].OneToMany_Optional_Self = new List<Level1> { l1s[8] };

            l1s[0].OneToOne_Optional_Self = l1s[9];
            l1s[1].OneToOne_Optional_Self = l1s[8];
            l1s[2].OneToOne_Optional_Self = l1s[7];
            l1s[3].OneToOne_Optional_Self = l1s[6];
            l1s[4].OneToOne_Optional_Self = l1s[5];

            l2s[0].OneToOne_Optional_PK = l3s[0];
            l2s[2].OneToOne_Optional_PK = l3s[2];
            l2s[5].OneToOne_Optional_PK = l3s[4];
            l2s[7].OneToOne_Optional_PK = l3s[6];
            l2s[9].OneToOne_Optional_PK = l3s[8];

            l2s[1].OneToOne_Optional_FK = l3s[8];
            l2s[3].OneToOne_Optional_FK = l3s[6];
            l2s[4].OneToOne_Optional_FK = l3s[4];
            l2s[6].OneToOne_Optional_FK = l3s[2];
            l2s[8].OneToOne_Optional_FK = l3s[0];

            l2s[0].OneToMany_Optional = new List<Level3> { l3s[1], l3s[5], l3s[9] };
            l2s[1].OneToMany_Optional = new List<Level3> { l3s[3], l3s[7] };

            l2s[1].OneToMany_Optional_Self = new List<Level2> { l2s[0] };
            l2s[3].OneToMany_Optional_Self = new List<Level2> { l2s[2] };
            l2s[5].OneToMany_Optional_Self = new List<Level2> { l2s[4] };
            l2s[7].OneToMany_Optional_Self = new List<Level2> { l2s[6] };
            l2s[9].OneToMany_Optional_Self = new List<Level2> { l2s[8] };

            l2s[0].OneToOne_Optional_Self = l2s[9];
            l2s[1].OneToOne_Optional_Self = l2s[8];
            l2s[2].OneToOne_Optional_Self = l2s[7];
            l2s[3].OneToOne_Optional_Self = l2s[6];
            l2s[4].OneToOne_Optional_Self = l2s[5];

            l3s[0].OneToOne_Optional_PK = l4s[0];
            l3s[2].OneToOne_Optional_PK = l4s[2];
            l3s[4].OneToOne_Optional_PK = l4s[4];
            l3s[6].OneToOne_Optional_PK = l4s[6];
            l3s[8].OneToOne_Optional_PK = l4s[8];

            l3s[1].OneToOne_Optional_FK = l4s[8];
            l3s[3].OneToOne_Optional_FK = l4s[6];
            l3s[5].OneToOne_Optional_FK = l4s[4];
            l3s[7].OneToOne_Optional_FK = l4s[2];
            l3s[9].OneToOne_Optional_FK = l4s[0];

            l3s[0].OneToMany_Optional = new List<Level4> { l4s[1], l4s[3], l4s[5], l4s[7], l4s[9] };

            l3s[1].OneToMany_Optional_Self = new List<Level3> { l3s[0] };
            l3s[3].OneToMany_Optional_Self = new List<Level3> { l3s[2] };
            l3s[5].OneToMany_Optional_Self = new List<Level3> { l3s[4] };
            l3s[7].OneToMany_Optional_Self = new List<Level3> { l3s[6] };
            l3s[9].OneToMany_Optional_Self = new List<Level3> { l3s[8] };

            l3s[0].OneToOne_Optional_Self = l3s[9];
            l3s[1].OneToOne_Optional_Self = l3s[8];
            l3s[2].OneToOne_Optional_Self = l3s[7];
            l3s[3].OneToOne_Optional_Self = l3s[6];
            l3s[4].OneToOne_Optional_Self = l3s[5];

            l4s[1].OneToMany_Optional_Self = new List<Level4> { l4s[0] };
            l4s[3].OneToMany_Optional_Self = new List<Level4> { l4s[2] };
            l4s[5].OneToMany_Optional_Self = new List<Level4> { l4s[4] };
            l4s[7].OneToMany_Optional_Self = new List<Level4> { l4s[6] };
            l4s[9].OneToMany_Optional_Self = new List<Level4> { l4s[8] };
        }

        public static void WireUpInversePart2(Level1[] l1s, Level2[] l2s, Level3[] l3s, Level4[] l4s)
        {
            l2s[0].OneToOne_Optional_PK_Inverse = l1s[0];
            l2s[2].OneToOne_Optional_PK_Inverse = l1s[2];
            l2s[4].OneToOne_Optional_PK_Inverse = l1s[4];
            l2s[6].OneToOne_Optional_PK_Inverse = l1s[6];
            l2s[8].OneToOne_Optional_PK_Inverse = l1s[8];

            l2s[8].OneToOne_Optional_FK_Inverse = l1s[1];
            l2s[6].OneToOne_Optional_FK_Inverse = l1s[3];
            l2s[4].OneToOne_Optional_FK_Inverse = l1s[5];
            l2s[2].OneToOne_Optional_FK_Inverse = l1s[7];
            l2s[0].OneToOne_Optional_FK_Inverse = l1s[9];

            l2s[8].Level1_Optional_Id = l1s[1].Id;
            l2s[6].Level1_Optional_Id = l1s[3].Id;
            l2s[4].Level1_Optional_Id = l1s[5].Id;
            l2s[2].Level1_Optional_Id = l1s[7].Id;
            l2s[0].Level1_Optional_Id = l1s[9].Id;

            l2s[1].OneToMany_Optional_Inverse = l1s[0];
            l2s[3].OneToMany_Optional_Inverse = l1s[0];
            l2s[5].OneToMany_Optional_Inverse = l1s[0];
            l2s[7].OneToMany_Optional_Inverse = l1s[0];
            l2s[9].OneToMany_Optional_Inverse = l1s[0];

            l1s[0].OneToMany_Optional_Self_Inverse = l1s[1];
            l1s[2].OneToMany_Optional_Self_Inverse = l1s[3];
            l1s[4].OneToMany_Optional_Self_Inverse = l1s[5];
            l1s[6].OneToMany_Optional_Self_Inverse = l1s[7];
            l1s[8].OneToMany_Optional_Self_Inverse = l1s[9];

            l3s[0].OneToOne_Optional_PK_Inverse = l2s[0];
            l3s[2].OneToOne_Optional_PK_Inverse = l2s[2];
            l3s[5].OneToOne_Optional_PK_Inverse = l2s[4];
            l3s[7].OneToOne_Optional_PK_Inverse = l2s[6];
            l3s[9].OneToOne_Optional_PK_Inverse = l2s[8];

            l3s[8].OneToOne_Optional_FK_Inverse = l2s[1];
            l3s[6].OneToOne_Optional_FK_Inverse = l2s[3];
            l3s[4].OneToOne_Optional_FK_Inverse = l2s[4];
            l3s[2].OneToOne_Optional_FK_Inverse = l2s[6];
            l3s[0].OneToOne_Optional_FK_Inverse = l2s[8];

            l3s[8].Level2_Optional_Id = l2s[1].Id;
            l3s[6].Level2_Optional_Id = l2s[3].Id;
            l3s[4].Level2_Optional_Id = l2s[4].Id;
            l3s[2].Level2_Optional_Id = l2s[6].Id;
            l3s[0].Level2_Optional_Id = l2s[8].Id;

            l3s[1].OneToMany_Optional_Inverse = l2s[0];
            l3s[5].OneToMany_Optional_Inverse = l2s[0];
            l3s[9].OneToMany_Optional_Inverse = l2s[0];
            l3s[3].OneToMany_Optional_Inverse = l2s[1];
            l3s[7].OneToMany_Optional_Inverse = l2s[1];

            l2s[0].OneToMany_Optional_Self_Inverse = l2s[1];
            l2s[2].OneToMany_Optional_Self_Inverse = l2s[3];
            l2s[4].OneToMany_Optional_Self_Inverse = l2s[5];
            l2s[6].OneToMany_Optional_Self_Inverse = l2s[7];
            l2s[8].OneToMany_Optional_Self_Inverse = l2s[9];

            l4s[0].OneToOne_Optional_PK_Inverse = l3s[0];
            l4s[2].OneToOne_Optional_PK_Inverse = l3s[2];
            l4s[4].OneToOne_Optional_PK_Inverse = l3s[4];
            l4s[6].OneToOne_Optional_PK_Inverse = l3s[6];
            l4s[8].OneToOne_Optional_PK_Inverse = l3s[8];

            l4s[8].OneToOne_Optional_FK_Inverse = l3s[1];
            l4s[6].OneToOne_Optional_FK_Inverse = l3s[3];
            l4s[4].OneToOne_Optional_FK_Inverse = l3s[5];
            l4s[2].OneToOne_Optional_FK_Inverse = l3s[7];
            l4s[0].OneToOne_Optional_FK_Inverse = l3s[9];

            l4s[8].Level3_Optional_Id = l3s[1].Id;
            l4s[6].Level3_Optional_Id = l3s[3].Id;
            l4s[4].Level3_Optional_Id = l3s[5].Id;
            l4s[2].Level3_Optional_Id = l3s[7].Id;
            l4s[0].Level3_Optional_Id = l3s[9].Id;

            l4s[1].OneToMany_Optional_Inverse = l3s[0];
            l4s[3].OneToMany_Optional_Inverse = l3s[0];
            l4s[5].OneToMany_Optional_Inverse = l3s[0];
            l4s[7].OneToMany_Optional_Inverse = l3s[0];
            l4s[9].OneToMany_Optional_Inverse = l3s[0];

            l3s[0].OneToMany_Optional_Self_Inverse = l3s[1];
            l3s[2].OneToMany_Optional_Self_Inverse = l3s[3];
            l3s[4].OneToMany_Optional_Self_Inverse = l3s[5];
            l3s[6].OneToMany_Optional_Self_Inverse = l3s[7];
            l3s[8].OneToMany_Optional_Self_Inverse = l3s[9];

            l4s[0].OneToMany_Optional_Self_Inverse = l4s[1];
            l4s[2].OneToMany_Optional_Self_Inverse = l4s[3];
            l4s[4].OneToMany_Optional_Self_Inverse = l4s[5];
            l4s[6].OneToMany_Optional_Self_Inverse = l4s[7];
            l4s[8].OneToMany_Optional_Self_Inverse = l4s[9];
        }

        public static IQueryable<T> Set<T>()
        {
            if (typeof(T) == typeof(Level1))
            {
                return new List<T>(_levelOnes.Cast<T>()).AsQueryable();
            }

            if (typeof(T) == typeof(Level2))
            {
                return new List<T>(_levelTwos.Cast<T>()).AsQueryable();
            }

            if (typeof(T) == typeof(Level3))
            {
                return new List<T>(_levelThrees.Cast<T>()).AsQueryable();
            }

            if (typeof(T) == typeof(Level4))
            {
                return new List<T>(_levelFours.Cast<T>()).AsQueryable();
            }

            throw new NotImplementedException();
        }
    }
}