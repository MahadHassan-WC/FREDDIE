using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 
   public class UTL_MVC_CONTROLLER_GENERATOR
    {
    private const String Q = @"""";
    private const String T1 = "\t";
    private const String T2 = "\t\t";
    private const String T3 = "\t\t\t";
    private const String T4 = "\t\t\t\t";
    private const String T5 = "\t\t\t\t\t";
    private const String T6 = "\t\t\t\t\t\t";
    private const String NL = "\n";
    private string _CS_CONTROLLER;
    public System.String CS_CONTROLLER
    {
        get { return _CS_CONTROLLER; }

    }

    public String GENERATE_INDEX_ACTION(String OBJECT_NAME)
        {
            string ModelName = UTL_GENERATE_MODEL_CLASS_NAME(OBJECT_NAME);
            string ReadClassName = UTL_GENERATE_READ_CLASS_NAME(OBJECT_NAME);
            _CS_CONTROLLER = "using Microsoft.AspNetCore.Mvc;" + Environment.NewLine +
            "public class " + ModelName + "Controller : Controller" + Environment.NewLine +
            "{" + Environment.NewLine +
            "public IActionResult Index()" + Environment.NewLine +
            "{" + Environment.NewLine +
            ReadClassName + " db = new " + ReadClassName + "();" + Environment.NewLine +
            "ViewData[" + Q + "dbdata" + Q + "] = db.GetAll();" + Environment.NewLine +
            "return View();" + Environment.NewLine +
             "}" + Environment.NewLine+
             "}" + Environment.NewLine;

        return _CS_CONTROLLER;
        }
        private string UTL_GENERATE_MODEL_CLASS_NAME(string OBJECT_NAME)
        {
            try
            {
                string MyString = null;
                string MyArrayElement = null;
                string[] MyArray = OBJECT_NAME.ToLower().Split('_');

                //MessageBox.Show(MyArray.Length.ToString());
                if (MyArray.Length > 1)
                {
                    for (int i = 0; i < MyArray.Length; i++)
                    {
                        MyArrayElement = MyArray[i];
                        for (int j = 0; j < MyArrayElement.Length; j++)
                        {
                            if (j == 0)
                            {
                                MyString += MyArrayElement.ToUpper().Substring(j, 1);
                            }
                            else

                            { MyString += MyArrayElement.Substring(j, 1); }

                        }

                    }
                }
                else
                {
                    MyArrayElement = OBJECT_NAME.ToLower();
                    for (int j = 0; j < MyArrayElement.Length; j++)
                    {
                        if (j == 0)
                        {
                            MyString += MyArrayElement.ToUpper().Substring(j, 1);
                        }
                        else

                        { MyString += MyArrayElement.Substring(j, 1); }

                    }
                }


                return MyString;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private string UTL_GENERATE_READ_CLASS_NAME(string OBJECT_NAME)
        {
            try
            {
                string MyString = null;
                string MyArrayElement = null;
                string[] MyArray = OBJECT_NAME.ToLower().Split('_');

                //MessageBox.Show(MyArray.Length.ToString());
                if (MyArray.Length > 1)
                {
                    for (int i = 0; i < MyArray.Length; i++)
                    {
                        MyArrayElement = MyArray[i];
                        for (int j = 0; j < MyArrayElement.Length; j++)
                        {
                            if (j == 0)
                            {
                                MyString += MyArrayElement.ToUpper().Substring(j, 1);
                            }
                            else

                            { MyString += MyArrayElement.Substring(j, 1); }

                        }

                    }
                }
                else
                {
                    MyArrayElement = OBJECT_NAME.ToLower();
                    for (int j = 0; j < MyArrayElement.Length; j++)
                    {
                        if (j == 0)
                        {
                            MyString += MyArrayElement.ToUpper().Substring(j, 1);
                        }
                        else

                        { MyString += MyArrayElement.Substring(j, 1); }

                    }
                }


                return MyString + "Read";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
}
 
