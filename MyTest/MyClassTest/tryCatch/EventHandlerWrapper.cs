using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace MyTest.MyClassTest
{
    public class EventHandlerWrapper
    {
        public object Target
        { get; private set; }

        public MethodInfo Method
        { get; private set; }

        public EventHandler Hander
        { get; private set; }

        public EventHandlerWrapper(EventHandler eventHandler)
        {
            if (null == eventHandler)
            {
                throw new ArgumentNullException("eventHandler");
            }
            //Parallel.Invoke(eventHandler,);
            this.Target = eventHandler.Target;
            this.Method = eventHandler.Method;
            this.Hander += Invoke;
        }

        public static implicit operator EventHandler (EventHandlerWrapper eventHandlerWrapper)
        {
            return eventHandlerWrapper.Hander;
        }

        private void Invoke(object sender, EventArgs args)
        {
            if(null !=  Form.ActiveForm)
            {
                Form.ActiveForm.Cursor = Cursors.WaitCursor;
            }
            try
            {
                this.Method.Invoke(this.Target, new object[] { sender, args });
            }
            catch (TargetInvocationException ex)
            {
                StringBuilder message = new StringBuilder();
                message.AppendLine(string.Format("Message: {0}", ex.InnerException.Message));
                message.AppendLine(string.Format("Exception Type: {0}", ex.InnerException.GetType().AssemblyQualifiedName));
                message.AppendLine(string.Format("Stack Trace: {0}", ex.InnerException.StackTrace));
                EventLog.WriteEntry("Application", message.ToString());
                MessageBox.Show(ex.InnerException.Message + Environment.NewLine + "For detailed information, please view event log", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (null != Form.ActiveForm)
                {
                    Form.ActiveForm.Cursor = Cursors.Default;
                }
            }
        }
    }
}