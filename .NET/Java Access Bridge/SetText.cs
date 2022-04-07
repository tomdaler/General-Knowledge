using System;

using System.ComponentModel;

using RPA.Core.Events;
using RPA.Plugin.Java;
using RPA.Core.Model;
using System.Diagnostics;
using RPA.Core;


namespace RPA_ATT_CannedNote
{
    class SetText
    {
        JavaEngine engine = JavaEngine.Instance;
        //3155916336

        private void InitializeAutomationControls()
        {
            WindowInfo windowInfo = Identifiers.GetWindowInfo(WindowIdentifer.MobilityWindow);

            if (windowInfo != null)
            {
                var container = engine.AddContainer(windowInfo.WindowTitle, windowInfo.Identifer, windowInfo.ProcessName);
                SearchCriteria mobileControlInfo = Identifiers.GetControlInfo(ControlIdentifer.MobileNo);
                SearchCriteria notesTextBoxControlInfo = Identifiers.GetControlInfo(ControlIdentifer.NotesTextBox);
                SearchCriteria byPassVerficationRadioButton = Identifiers.GetControlInfo(ControlIdentifer.ByPassVerficationRadioButton);

                if (container != null)
                {
                    if (mobileControlInfo != null)
                    {
                        container.EditControl(ControlIdentifer.MobileNo, mobileControlInfo);
                    }


                    if (byPassVerficationRadioButton != null)
                    {
                        container.RadioButton(ControlIdentifer.ByPassVerficationRadioButton, byPassVerficationRadioButton);
                    }

                    if (notesTextBoxControlInfo != null)
                    {
                        var control = container.EditControl(ControlIdentifer.NotesTextBox, notesTextBoxControlInfo);
                        control.EventToMonitor.TextChange = false;
                    }
                }
            }
        }


        public void SetClarifyText()
        {
            InitializeAutomationControls();
            var container = engine.FindContainer(WindowIdentifer.MobilityWindow);

            if (container != null)
            {
                var control = container.EditControl(ControlIdentifer.MobileNo);
                if (control != null)
                {
                  //  txtNewValue.Text = control.GetValue();
                }
            }
        }


        public bool WriteClarifyText(string Texto)
        {
                InitializeAutomationControls();

                var container = engine.FindContainer(WindowIdentifer.MobilityWindow);
                if (container == null) return false;

                var control = container.EditControl(ControlIdentifer.NotesTextBox);
                if (control == null) return false;

                control.SetValue(Texto);
                return true;
       }
    }
}
