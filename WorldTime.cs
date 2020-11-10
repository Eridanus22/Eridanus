using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace Eridanus
{
    public class WorldTime
    {
        //360 days in a year
        short year, month, day, hours, mins, secs;

        public WorldTime() {
            year = 2100;
            month = 1;
            day = 1;
            hours = 0;
            mins = 0;
            secs = 0;
        }

        public WorldTime(short yr, short mon)
        {
            year = yr;
            month = mon;
            day = 1;
            hours = 0;
            mins = 0;
            secs = 0;
        }

        public void increment()
        {
            secs++;
            if (secs >= 60)
            {
                secs = 0;
                mins++;
                if (mins >= 60)
                {
                    mins = 0;
                    hours++;
                    if (hours >= 24)
                    {
                        hours = 0;
                        day++;
                        if (day > 30)
                        {
                            day = 1;
                            month++;
                            if (month > 12)
                            {
                                month = 1;
                                year++;
                            }
                        }
                    }
                }
            }
        }

        public String toString()
        {
            String date = makeTwo(day) + " " + getMon() + " " + year + "  " + makeTwo(hours) + ":" + makeTwo(mins) + ":" + makeTwo(secs);
                return date;
        }

        public String makeTwo(short n)
        {
            if (n < 10) { return "0" + n.ToString(); }
            else { return n.ToString(); }
        }

        public String getMon()
        {
            if (month < 6)
            {
                if (month < 3)
                {
                    if (month == 1) { return "JAN";  }
                    else { return "FEB";  }
                }
                else // 6>n>3
                {
                    if (month == 3) { return "MAR";  }
                    else if (month == 4) { return "APR"; }
                    else { return "MAY";  }
                }
            }
            else{   //n>5
                if (month < 9)
                {
                    if (month == 6) { return "JUN";  }
                    else if (month == 7) { return "JUL"; }
                    else{ return "AUG"; }
                }
                else
                {
                    if (month == 9) { return "SEP"; }
                    else if (month == 10) { return "OCT"; }
                    else if (month == 11) { return "NOV"; }
                    else { return "DEC";  }
                }
            }
        }

        public Boolean newDay() { if (hours == 0 && mins == 0 && secs == 0) { return true; } else { return false; } }
        public Boolean newHour() { if (mins == 0 && secs == 0) { return true; } else { return false; } }

        public Boolean newMinute() { if (secs == 0) { return true; } else { return false; } }
    }
}
