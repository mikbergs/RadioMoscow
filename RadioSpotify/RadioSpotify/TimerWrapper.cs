using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RadioSpotify
{
    public class TimerWrapper
    {
        private DateTime _date;
        private static Timer _timer;
        public Timer Timer { get { return _timer; } }
        public TimerWrapper()
        {
            _timer = new Timer();
            _timer.Enabled = true;
            _timer.Interval = Constants.frequent;
            _date = DateTime.Now;

        }
        private static void SetTimer(int duration)
        {
            _timer.Interval = duration;            
        }
        public async Task ScheduleAction(Action action, DateTime executionTime, DateTime internalTime)
        {
            try
            {
            await Task.Delay((int)executionTime.Subtract(internalTime).TotalMilliseconds);
            action();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }        
        }
    }
}
