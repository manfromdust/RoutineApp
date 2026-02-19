using System;
using System.Collections.Generic;
using System.Text;

namespace HW00
{
    internal class TimeFrame
    {
        public int from;
        public int to;
        public string day;

        public TimeFrame(int begin, int end, string day)
        {
            from = begin;
            to = end;
            this.day = day;
        }
    }
}
