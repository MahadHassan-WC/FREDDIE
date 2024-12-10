using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;



public class UTL_CS_GUI_GENERATOR
    {



    private const String Q = @"""";
    private const String T1 = "\t";
    private const String T2 = "\t\t";
    private const String T3 = "\t\t\t";
    private const String T4 = "\t\t\t\t";
    private const String T5 = "\t\t\t\t\t";
    private const String NL = "\n";



    private String _CS_GET_DATA_FROM_GRID;
    private String _CS_UPDATE_A_RECORD;
    private String _CS_DELETE_A_RECORD;
    private String _CS_LOAD_GRID;
    private String _CS_RECORD_IS_VALID;
    private String _CS_RESET_SCREEN;
    private String _CS_MODULE_LEVEL_VARIABLES;
    private String _CS_TEXT_CHANGED;


    public System.String CS_TEXT_CHANGED
    {
        get { return _CS_TEXT_CHANGED; }
        set { _CS_TEXT_CHANGED = value; }

    }

    public System.String CS_MODULE_LEVEL_VARIABLES
    {
        get { return _CS_MODULE_LEVEL_VARIABLES; }
        set { _CS_MODULE_LEVEL_VARIABLES = value; }

    }


    public System.String CS_RECORD_IS_VALID
    {
        get { return _CS_RECORD_IS_VALID; }
        set { _CS_RECORD_IS_VALID = value; }

    }


    public System.String CS_RESET_SCREEN
    {
        get { return _CS_RESET_SCREEN; }
        set { _CS_RESET_SCREEN = value; }

    }



    public System.String CS_UPDATE_A_RECORD
    {
        get { return _CS_UPDATE_A_RECORD; }
        set { _CS_UPDATE_A_RECORD = value; }

    }

    public System.String CS_DELETE_A_RECORD
    {
        get { return _CS_DELETE_A_RECORD; }
        set { _CS_DELETE_A_RECORD = value; }

    }

    public System.String CS_GET_DATA_FROM_GRID
    {
        get { return _CS_GET_DATA_FROM_GRID; }
        set { _CS_GET_DATA_FROM_GRID = value; }

    }


    public System.String CS_LOAD_GRID
    {
        get { return _CS_LOAD_GRID; }
        set { _CS_LOAD_GRID = value; }

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


            return MyString + "Model";

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
    private string UTL_GENERATE_IUD_CLASS_NAME(string OBJECT_NAME)
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


            return MyString + "IUD";

        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    public void GENERATE_GUI_FUNCTIONS(String SERVER_NAME, String DATABASE_NAME, Int32 OBJECT_ID, String OBJECT_NAME, Boolean View)
    {
        
        UTL_CS_GENERATOR utl = new UTL_CS_GENERATOR();

        if (View)
        {
            List<FREDS_MATRIX> ATTRIBUTES = utl.UTL_GET_ATTRIBUTES_VIEW(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID);
           

        }
        else
        {
            List<FREDS_MATRIX> ATTRIBUTES = utl.UTL_GET_ATTRIBUTES_TABLE(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID, false);
            List<FREDS_MATRIX> ATTRIBUTES_PK = utl.UTL_GET_ATTRIBUTES_TABLE(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID, true);
            this.CS_GET_DATA_FROM_GRID = GENERATE_GET_DATA_FROM_GRID(ATTRIBUTES);
            this.CS_UPDATE_A_RECORD = GENERATE_UPDATE_A_RECORD(ATTRIBUTES, ATTRIBUTES_PK, OBJECT_NAME);
            this.CS_DELETE_A_RECORD = GENERATE_DELETE_A_RECORD(ATTRIBUTES, ATTRIBUTES_PK, OBJECT_NAME);
            this.CS_LOAD_GRID = GENERATE_LOAD_GRID(OBJECT_NAME);
            this.CS_RESET_SCREEN = GENERATE_RESET_SCREEN(ATTRIBUTES, ATTRIBUTES_PK, OBJECT_NAME);
            this.CS_RECORD_IS_VALID = GENERATE_RECORD_IS_VALID(ATTRIBUTES);
            this.CS_MODULE_LEVEL_VARIABLES = GENERATE_MODULE_LEVEL_VARIABLES();
            this.CS_TEXT_CHANGED= GENERATE_TEXT_CHANGED(ATTRIBUTES);

        }
      
    }


    private string GENERATE_MODULE_LEVEL_CONTROLS(List<FREDS_MATRIX> COLUMNS)
    {
        String MyString = null;
        foreach (FREDS_MATRIX f in COLUMNS)
        {


            MyString += T4 + "TextBox txt" + f.COLUMN_NAME + "= new TextBox();" + Environment.NewLine;
            MyString += T4 + "Label lbl" + f.COLUMN_NAME + "= new Label();" + Environment.NewLine;


        }

        return MyString;
    }
    private string GENERATE_ADDTEXBOXES(List<FREDS_MATRIX> COLUMNS)
    {
        String MyString = null;


        int counter = 0;



        int X_LABEL_R1 = 12;
        int Y_LABEL_R1 = 500;


        int X_TEXT_BOX_R1 = 12;
        int Y_TEXT_BOX_R1 = 520;






        int X_LABEL_R2 = 12;
        int Y_LABEL_R2 = 550;


        int X_TEXT_BOX_R2 = 12;
        int Y_TEXT_BOX_R2 = 570;




        MyString += T1 + "void AddTextBoxes()" + Environment.NewLine;
        MyString += T1 + "{" + Environment.NewLine;
        MyString += T2 + "try" + Environment.NewLine;
        MyString += T2 + "{" + Environment.NewLine;
        foreach (FREDS_MATRIX f in COLUMNS)
        {

            counter++;


            if(counter <= 10)
            {


                MyString += T4 + "lbl" + f.COLUMN_NAME + ".Location = new System.Drawing.Point(" + X_LABEL_R1 + ", " + (Y_LABEL_R1) + "); " + Environment.NewLine;
                MyString += T4 + "lbl" + f.COLUMN_NAME + ".Size = new System.Drawing.Size(150, 20); " + Environment.NewLine;
                MyString += T4 + "lbl" + f.COLUMN_NAME + ".Name =" + Q + "lbl" + f.COLUMN_NAME + Q + ";" + Environment.NewLine;
                MyString += T4 + "lbl" + f.COLUMN_NAME + ".Text =" + Q + f.COLUMN_NAME + Q + ";" + Environment.NewLine;
                MyString += T4 + "this.Controls.Add(lbl" + f.COLUMN_NAME + "); " + Environment.NewLine + Environment.NewLine;



                MyString += T4 + "txt" + f.COLUMN_NAME + ".Location = new System.Drawing.Point(" + X_TEXT_BOX_R1 + ", " + Y_TEXT_BOX_R1 + "); " + Environment.NewLine;
                MyString += T4 + "txt" + f.COLUMN_NAME + ".Size = new System.Drawing.Size(150, 20); " + Environment.NewLine;
                MyString += T4 + "txt" + f.COLUMN_NAME + ".Name =" + Q + "txt" + f.COLUMN_NAME + Q + ";" + Environment.NewLine;
                MyString += T4 + "this.Controls.Add(txt" + f.COLUMN_NAME + "); " + Environment.NewLine + Environment.NewLine;


                X_LABEL_R1 += 155;
                X_TEXT_BOX_R1 += 155;


            }
            else
            {
                MyString += T4 + "lbl" + f.COLUMN_NAME + ".Location = new System.Drawing.Point(" + X_LABEL_R2 + ", " + (Y_LABEL_R2) + "); " + Environment.NewLine;
                MyString += T4 + "lbl" + f.COLUMN_NAME + ".Size = new System.Drawing.Size(150, 20); " + Environment.NewLine;
                MyString += T4 + "lbl" + f.COLUMN_NAME + ".Name =" + Q + "lbl" + f.COLUMN_NAME + Q + ";" + Environment.NewLine;
                MyString += T4 + "lbl" + f.COLUMN_NAME + ".Text =" + Q + f.COLUMN_NAME + Q + ";" + Environment.NewLine;
                MyString += T4 + "this.Controls.Add(lbl" + f.COLUMN_NAME + "); " + Environment.NewLine + Environment.NewLine;



                MyString += T4 + "txt" + f.COLUMN_NAME + ".Location = new System.Drawing.Point(" + X_TEXT_BOX_R2 + ", " + Y_TEXT_BOX_R2 + "); " + Environment.NewLine;
                MyString += T4 + "txt" + f.COLUMN_NAME + ".Size = new System.Drawing.Size(150, 20); " + Environment.NewLine;
                MyString += T4 + "txt" + f.COLUMN_NAME + ".Name =" + Q + "txt" + f.COLUMN_NAME + Q + ";" + Environment.NewLine;
                MyString += T4 + "this.Controls.Add(txt" + f.COLUMN_NAME + "); " + Environment.NewLine + Environment.NewLine;


                X_LABEL_R2 += 155;
                X_TEXT_BOX_R2 += 155;

            }



        }
        MyString += T2 + "}" + Environment.NewLine;
        MyString += UTL_GENERATE_CATCH_SECTION_OF_METHOD();
        return MyString;



    }


    private string GENERATE_LOAD_GRID(  string OBJECT_NAME)

    {
        String MyString = null;




        MyString += T1 + "private void LoadGrid()" + Environment.NewLine;
        MyString += T1 + "{" + Environment.NewLine;

       
       


        MyString += T2 + "try" + Environment.NewLine;
        MyString += T2 + "{" + Environment.NewLine;

        MyString += T1 + "this.Cursor = Cursors.WaitCursor;" + Environment.NewLine;
        MyString += T1 + UTL_GENERATE_READ_CLASS_NAME(OBJECT_NAME) + " db = new " + UTL_GENERATE_READ_CLASS_NAME(OBJECT_NAME) + "();" + Environment.NewLine;
        MyString += T1 + "DataSet ds = db.SEL_ALL();" + Environment.NewLine;
        MyString += T1 + "dataGridView1.DataSource = ds.Tables[0];" + Environment.NewLine;
        MyString += T1 + "dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;" + Environment.NewLine;
        MyString += T1 + "dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;" + Environment.NewLine;
        MyString += T1 + "dataGridView1.ReadOnly = true;" + Environment.NewLine;
        MyString += T1 + "dataGridView1.AllowUserToAddRows = false;" + Environment.NewLine;
        MyString += T1 + "dataGridView1.AllowUserToDeleteRows = false;" + Environment.NewLine;
        MyString += T1 + "dataGridView1.RowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;" + Environment.NewLine;
        MyString += T1 + "dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.WhiteSmoke;" + Environment.NewLine;


        MyString += T1 + "}" + Environment.NewLine;

        MyString += UTL_GENERATE_CATCH_SECTION_OF_METHOD2();


        return MyString;
    }

    private string GENERATE_DELETE_A_RECORD(List<FREDS_MATRIX> COLUMNS, List<FREDS_MATRIX> PK, string OBJECT_NAME)
    {
        String MyString = null;


        MyString += T1 + "private void Delete_A_Record()" + Environment.NewLine;
        MyString += T1 + "{" + Environment.NewLine;
        MyString += T2 + "try" + Environment.NewLine;
        MyString += T2 + "{" + Environment.NewLine;
        MyString += T2 + "if (bHaveKey)" + Environment.NewLine;
        MyString += T2 + "{" + Environment.NewLine;





        MyString += T3 + UTL_GENERATE_MODEL_CLASS_NAME(OBJECT_NAME) + " m = new  " + UTL_GENERATE_MODEL_CLASS_NAME(OBJECT_NAME) + Environment.NewLine;
        MyString += T3 + "{" + Environment.NewLine;
        foreach (FREDS_MATRIX k in PK)
        {
            switch (k.CS_DATA_TYPE)
            {
                case "System.DateTime":
                    MyString += T3 +   k.COLUMN_NAME + " =Convert.ToDateTime(txt" + k.COLUMN_NAME + ".Text.Trim())," + Environment.NewLine;
                    break;
                case "System.Int32":
                    MyString += T3 +   k.COLUMN_NAME + " =Convert.ToInt32(txt" + k.COLUMN_NAME + ".Text.Trim())," + Environment.NewLine;
                    break;
                case "System.String":
                    MyString += T3 +     k.COLUMN_NAME + " = txt" + k.COLUMN_NAME + ".Text.Trim()," + Environment.NewLine;
                    break;


            }
        }
        MyString += T3 + "};" + Environment.NewLine;



        MyString += T2 + UTL_GENERATE_IUD_CLASS_NAME(OBJECT_NAME) + " iud = new  " + UTL_GENERATE_IUD_CLASS_NAME(OBJECT_NAME) + "();" + Environment.NewLine;




        MyString += T2 + "if (iud.DEL_A_ROW_BY_PK(m))" + Environment.NewLine;
        MyString += T2 + "{" + Environment.NewLine;
        MyString += T2 + "MessageBox.Show(" + Q + "Record DELETED from database" + Q + "," + Q + "Database Updated" + Q + ", MessageBoxButtons.OK, MessageBoxIcon.Information);" + Environment.NewLine;
        MyString += T2 + "}" + Environment.NewLine;
        MyString += T2 + "else" + Environment.NewLine;
        MyString += T2 + "{" + Environment.NewLine;
        MyString += T2 + "MessageBox.Show(" + Q + "Error raised on DELETE" + Q + "," + Q + "Database Error" + Q + ", MessageBoxButtons.OK, MessageBoxIcon.Error);" + Environment.NewLine;
        MyString += T2 + "}" + Environment.NewLine;


        MyString += T2 + "m = null;" + Environment.NewLine;
        MyString += T2 + "iud = null;" + Environment.NewLine;
        MyString += T2 + "LoadGrid();" + Environment.NewLine;
        MyString += T2 + "ResetScreen();" + Environment.NewLine;










        MyString += T3 + "}" + Environment.NewLine;


        MyString += T2 + "}" + Environment.NewLine;


        MyString += UTL_GENERATE_CATCH_SECTION_OF_METHOD();



        return MyString;
    }

    private string GENERATE_RECORD_IS_VALID(List<FREDS_MATRIX> COLUMNS)
    {
        String MyString = null;


        MyString += T1 + "private bool Record_Is_Valid()" + Environment.NewLine;
        MyString += T1 + "{" + Environment.NewLine;
        MyString += T2 + "try" + Environment.NewLine;
        MyString += T2 + "{" + Environment.NewLine;

        MyString += T3 + "int errCount = 0;" + Environment.NewLine;
        MyString += T3 + "ValidationMSG =" + Q + "The following error(s) were caught. Please enter required data:" + Q + "+ Environment.NewLine + Environment.NewLine;"+ Environment.NewLine;





        foreach (FREDS_MATRIX f in COLUMNS)
        {
            if(f.RESERVED_YN == "N")
            {
                MyString += T4 + "if (txt" + f.COLUMN_NAME + ".Text.Length == 0)" + Environment.NewLine;
                MyString += T4 + "{" + Environment.NewLine;
                MyString += T4 + "ValidationMSG +=  " + Q + f.COLUMN_NAME +  " is missing!" + Q + "+ Environment.NewLine;" + Environment.NewLine;
                MyString += T4 + "errCount++;" + Environment.NewLine;
                MyString += T4 + "}" + Environment.NewLine;
            }
           
            
        }




        MyString += T3 + "if (errCount == 0)" + Environment.NewLine;
        MyString += T3 + "{" + Environment.NewLine;
        MyString += T3 + "return true;" + Environment.NewLine;
        MyString += T3 + "}" + Environment.NewLine;
        MyString += T3 + "else" + Environment.NewLine;
        MyString += T3 + "{" + Environment.NewLine;
        MyString += T3 + "return false;" + Environment.NewLine;
        MyString += T3 + "}" + Environment.NewLine;
       


        MyString += T1 + "}" + Environment.NewLine;

        MyString += UTL_GENERATE_CATCH_SECTION_OF_METHOD3();

        return MyString;

    }


    private string GENERATE_RESET_SCREEN(List<FREDS_MATRIX> COLUMNS, List<FREDS_MATRIX> PK, string OBJECT_NAME)
    {
        String MyString = null;


        MyString += T1 + "private void ResetScreen()" + Environment.NewLine;
        MyString += T1 + "{" + Environment.NewLine;
        MyString += T2 + "try" + Environment.NewLine;
        MyString += T2 + "{" + Environment.NewLine;


        foreach (FREDS_MATRIX f in COLUMNS)
        {
            MyString += T4 + "txt" + f.COLUMN_NAME + ".Clear();" + Environment.NewLine;
        }




        foreach (FREDS_MATRIX f in COLUMNS)
        {
            if (f.RESERVED_YN == "Y" || f.PK_YN == "Y")
            {
                MyString += T4 + "txt" + f.COLUMN_NAME + ".BackColor = READ_ONLY_textBoxBackgroundColor;" + Environment.NewLine;
            }
            else
            {
                MyString += T4 + "txt" + f.COLUMN_NAME + ".BackColor = DEFAULT_textBoxBackgroundColor;" + Environment.NewLine;
            }
        }




        MyString += T4 + "btnUPDATE.Enabled = false;" + Environment.NewLine;
        MyString += T4 + "btnDELETE.Enabled = false;" + Environment.NewLine;
        MyString += T4 + "bHaveKey = false;" + Environment.NewLine;


        MyString += T1 + "}" + Environment.NewLine;

        MyString += UTL_GENERATE_CATCH_SECTION_OF_METHOD();


        return MyString;


    }




    private string GENERATE_MODULE_LEVEL_VARIABLES()
    {
        String MyString = null;


        MyString += T1 + "private bool bHaveKey = false;" + Environment.NewLine;
        MyString += T1 + "private string ValidationMSG = null;" + Environment.NewLine;
        MyString += T1 + "private Color DEFAULT_textBoxBackgroundColor = Color.FromArgb(192, 255, 192);" + Environment.NewLine;
        MyString += T1 + "private Color UPDATE_textBoxBackgroundColor = Color.FromArgb(192, 255, 255);" + Environment.NewLine;
        MyString += T1 + "private Color READ_ONLY_textBoxBackgroundColor = Color.FromArgb(224, 224, 224);" + Environment.NewLine;


        return MyString;
    }





    private string GENERATE_UPDATE_A_RECORD(List<FREDS_MATRIX> COLUMNS, List<FREDS_MATRIX> PK, string OBJECT_NAME)
    {
        String MyString = null;



        MyString += T1 + "private void Update_A_Record()" + Environment.NewLine;
        MyString += T1 + "{" + Environment.NewLine;
        MyString += T2 + "try" + Environment.NewLine;
        MyString += T2 + "{" + Environment.NewLine;


     

        MyString += T2 + "if (Record_Is_Valid())" + Environment.NewLine;
        MyString += T2 + "{" + Environment.NewLine;
        MyString += T3 + UTL_GENERATE_MODEL_CLASS_NAME(OBJECT_NAME) + " m = new  " + UTL_GENERATE_MODEL_CLASS_NAME(OBJECT_NAME) + Environment.NewLine;
        MyString += T3 + "{" + Environment.NewLine;


        foreach (FREDS_MATRIX f in COLUMNS)
        {
            if (f.PK_YN == "N"  &&  f.RESERVED_YN == "N")
            {
                switch (f.CS_DATA_TYPE)
                {
                    case "System.DateTime":
                        MyString += T3 + f.COLUMN_NAME + " =Convert.ToDateTime(txt" + f.COLUMN_NAME + ".Text.Trim())," + Environment.NewLine;
                        break;
                    case "System.Int32":
                        MyString += T3 + f.COLUMN_NAME + " =Convert.ToInt32(txt" + f.COLUMN_NAME + ".Text.Trim())," + Environment.NewLine;
                        break;
                    case "System.String":
                        MyString += T3 + f.COLUMN_NAME + " = txt" + f.COLUMN_NAME + ".Text.Trim()," + Environment.NewLine;
                        break;
                     

                }
              
            }
        }
        MyString += T3 + "};" + Environment.NewLine;  ///end of create model class

        MyString += T2 + UTL_GENERATE_IUD_CLASS_NAME(OBJECT_NAME) + " iud = new  " + UTL_GENERATE_IUD_CLASS_NAME(OBJECT_NAME) + "();" + Environment.NewLine;


        MyString += T2 + "if (bHaveKey)" + Environment.NewLine;

        MyString += T2 + "{" + Environment.NewLine;


        foreach (FREDS_MATRIX k in PK)
        {
            switch (k.CS_DATA_TYPE)
            {
                case "System.DateTime":
                    MyString += T3 + "m." + k.COLUMN_NAME + " =Convert.ToDateTime(txt" + k.COLUMN_NAME + ".Text.Trim());" + Environment.NewLine;
                    break;
                case "System.Int32":
                    MyString += T3 + "m." + k.COLUMN_NAME + " =Convert.ToInt32(txt" + k.COLUMN_NAME + ".Text.Trim());" + Environment.NewLine;
                    break;
                case "System.String":
                    MyString += T3 + "m." + k.COLUMN_NAME + " = txt" + k.COLUMN_NAME + ".Text.Trim();" + Environment.NewLine;
                    break;


            }
        }

        MyString += T2 + "if (iud.UPD_A_ROW_BY_PK(m))" + Environment.NewLine;
        MyString += T2 + "{" + Environment.NewLine;
        MyString += T2 + "MessageBox.Show(" + Q + "Record UPDATED in database" + Q + "," + Q + "Database Updated" + Q + ", MessageBoxButtons.OK, MessageBoxIcon.Information);" + Environment.NewLine;
        MyString += T2 + "}" + Environment.NewLine;
        MyString += T2 + "else" + Environment.NewLine;
        MyString += T2 + "{" + Environment.NewLine;
        MyString += T2 + "MessageBox.Show(" + Q + "Error raised on UPDATE" + Q +"," + Q + "Database Error" + Q + ", MessageBoxButtons.OK, MessageBoxIcon.Error);" + Environment.NewLine;
        MyString += T2 + "}" + Environment.NewLine;


        MyString += T2 + "}" + Environment.NewLine;

        MyString += T2 + "else" + Environment.NewLine;

        MyString += T2 + "{" + Environment.NewLine;


        MyString += T2 + "if (iud.INS_A_ROW(m))" + Environment.NewLine;
        MyString += T2 + "{" + Environment.NewLine;
        MyString += T2 + "MessageBox.Show(" + Q + "Record INSERTED in database" + Q + "," + Q + "Database Updated" + Q + ", MessageBoxButtons.OK, MessageBoxIcon.Information);" + Environment.NewLine;
        MyString += T2 + "}" + Environment.NewLine;
        MyString += T2 + "else" + Environment.NewLine;
        MyString += T2 + "{" + Environment.NewLine;
        MyString += T2 + "MessageBox.Show(" + Q + "Error raised on INSERT" + Q + "," + Q + "Database Error" + Q + ", MessageBoxButtons.OK, MessageBoxIcon.Error);" + Environment.NewLine;
        MyString += T2 + "}" + Environment.NewLine;
        MyString += T2 + "}" + Environment.NewLine;


        MyString += T2 + "iud = null;" + Environment.NewLine;
        MyString += T2 + "m = null;" + Environment.NewLine;
        MyString += T2 + "LoadGrid();" + Environment.NewLine;
        MyString += T2 + "ResetScreen();" + Environment.NewLine;



        MyString += T2 + "}" + Environment.NewLine;  ///end record is valid
        MyString += T1 + "else" + Environment.NewLine;  ///  record is valid

        MyString += T2 + "{" + Environment.NewLine;

        MyString += T2 + "MessageBox.Show(ValidationMSG," + Q + "Data Validation Error" + Q + ", MessageBoxButtons.OK, MessageBoxIcon.Error);" + Environment.NewLine;

        MyString += T2 + "}" + Environment.NewLine;



        MyString += T2 + "}" + Environment.NewLine;
        MyString += UTL_GENERATE_CATCH_SECTION_OF_METHOD();

        return MyString;
    }





    private string GENERATE_TEXT_CHANGED(List<FREDS_MATRIX> COLUMNS)
    {


        String MyString = null;

        foreach (FREDS_MATRIX f in COLUMNS)
        {
            if (f.RESERVED_YN == "Y" || f.PK_YN == "Y")
            {
                
            }
            else
            {
                //MyString += T4 + "private void txt" + f.COLUMN_NAME + "_TextChanged(object sender, EventArgs e)" + Environment.NewLine;
                //MyString += T1 + "{" + Environment.NewLine;
                MyString += T1 + "// TODO: add this code to this event: private void txt" + f.COLUMN_NAME + "_TextChanged(object sender, EventArgs e)" + Environment.NewLine;
                MyString += T1 + "btnUPDATE.Enabled = true;" + Environment.NewLine;
                MyString += T1 + "txt"  + f.COLUMN_NAME +  ".BackColor = UPDATE_textBoxBackgroundColor;" + Environment.NewLine;

                //MyString += T1 + "}" + Environment.NewLine;
            }
        }

        return MyString;
    }





    private string GENERATE_GET_DATA_FROM_GRID(List<FREDS_MATRIX> COLUMNS )
    {


        String MyString = null;

        MyString += T1 + "private void GetDataFromGrid(DataGridView GRD)" + Environment.NewLine;
        MyString += T1 + "{" + Environment.NewLine;
        MyString += T2 + "try" + Environment.NewLine;
        MyString += T2 + "{" + Environment.NewLine;
        MyString += T3 + "ResetScreen();" + Environment.NewLine;
        MyString += T3 + "foreach (DataGridViewRow row in GRD.SelectedRows)" + Environment.NewLine;
        MyString += T3 + "{" + Environment.NewLine;

        foreach (FREDS_MATRIX f in COLUMNS)
        {
            
             
                MyString += T4 + "txt" + f.COLUMN_NAME + ".Text = (GRD.SelectedRows[0].Cells[" + Q + f.COLUMN_NAME + Q + "].Value.ToString());" + Environment.NewLine;
                if(f.RESERVED_YN == "Y" || f.PK_YN == "Y")
                {
                    MyString += T4 + "txt" + f.COLUMN_NAME + ".BackColor = READ_ONLY_textBoxBackgroundColor;" + Environment.NewLine;
                }
                else
                {
                    MyString += T4 + "txt" + f.COLUMN_NAME + ".BackColor = DEFAULT_textBoxBackgroundColor;" + Environment.NewLine;
                }
            
               

        }



        MyString += T4 + "btnUPDATE.Enabled = true;" + Environment.NewLine;
        MyString += T4 + "btnDELETE.Enabled = true;" + Environment.NewLine;
        MyString += T4 + "bHaveKey = true;" + Environment.NewLine;
        MyString += T4 + " " + Environment.NewLine;
        MyString += T4 + " " + Environment.NewLine;


        MyString += T3 + "}" + Environment.NewLine;

        MyString += T2 + "}" + Environment.NewLine;
        MyString += UTL_GENERATE_CATCH_SECTION_OF_METHOD();


        return MyString;

    }

    private String UTL_GENERATE_CATCH_SECTION_OF_METHOD()
    {
        String s = null;
        s += T2 + "catch (Exception ex)" + Environment.NewLine;
        s += T2 + "{" + Environment.NewLine;
        s += T3 + "MessageBox.Show(" + Q + "Error MSG: " + Q + "+ ex.Message + Environment.NewLine +" + Environment.NewLine;
        s += T3 + Q + "METHOD NAME: " + Q + "+ System.Reflection.MethodBase.GetCurrentMethod().Name, " + Q + " ERROR " + Q + ", MessageBoxButtons.OK, MessageBoxIcon.Error);" + Environment.NewLine;

        s += T2 + "}" + Environment.NewLine;
        s += T2 + "finally" + Environment.NewLine;
        s += T2 + "{" + Environment.NewLine;


        s += T2 + "}" + Environment.NewLine;
        s += T1 + "}" + Environment.NewLine + Environment.NewLine;
        return s;
    }

    private String UTL_GENERATE_CATCH_SECTION_OF_METHOD2()
    {
        String s = null;
        s += T2 + "catch (Exception ex)" + Environment.NewLine;
        s += T2 + "{" + Environment.NewLine;
        s += T3 + "MessageBox.Show(" + Q + "Error MSG: " + Q + "+ ex.Message + Environment.NewLine +" + Environment.NewLine;
        s += T3 + Q + "METHOD NAME: " + Q + "+ System.Reflection.MethodBase.GetCurrentMethod().Name, " + Q + " ERROR " + Q + ", MessageBoxButtons.OK, MessageBoxIcon.Error);" + Environment.NewLine;

        s += T2 + "}" + Environment.NewLine;
        s += T2 + "finally" + Environment.NewLine;
        s += T2 + "{" + Environment.NewLine;
        s += T2 + "this.Cursor = Cursors.Default;" + Environment.NewLine;

        s += T2 + "}" + Environment.NewLine;
        s += T1 + "}" + Environment.NewLine + Environment.NewLine;
        return s;
    }

    private String UTL_GENERATE_CATCH_SECTION_OF_METHOD3()
    {
        String s = null;
        s += T2 + "catch (Exception ex)" + Environment.NewLine;
        s += T2 + "{" + Environment.NewLine;
        s += T3 + "MessageBox.Show(" + Q + "Error MSG: " + Q + "+ ex.Message + Environment.NewLine +" + Environment.NewLine;
        s += T3 + Q + "METHOD NAME: " + Q + "+ System.Reflection.MethodBase.GetCurrentMethod().Name, " + Q + " ERROR " + Q + ", MessageBoxButtons.OK, MessageBoxIcon.Error);" + Environment.NewLine;
        s += T3 + "return false;" + Environment.NewLine;
        s += T2 + "}" + Environment.NewLine;
        s += T2 + "finally" + Environment.NewLine;
        s += T2 + "{" + Environment.NewLine;
        s += T2 + "this.Cursor = Cursors.Default;" + Environment.NewLine;

        s += T2 + "}" + Environment.NewLine;
        s += T1 + "}" + Environment.NewLine + Environment.NewLine;
        return s;
    }

}

