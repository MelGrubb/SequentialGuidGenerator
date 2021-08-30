using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SequentialGuidGenerator
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Generate()
        {
            ResultTextBox.Clear();

            for(var x = 0; x<quantityUpDown.Value; x++)
            {
                var value = NewSequentialGuid().ToString("D");
                value = value.ToUpper();

                ResultTextBox.Text += value + Environment.NewLine;
            }

            Clipboard.SetText(ResultTextBox.Text);


            //var guid = NewSequentialGuid();
            //var text = guid.ToString("D").ToUpper();
            //ResultTextBox.Text = text;
            //Clipboard.SetText(text);
        }

        private void GenerateButton_Click(object sender, EventArgs e) => Generate();

        /// <summary>Creates a sequential GUID.</summary>
        /// <returns>A GUID which is greater than values previously generated on the same machine.</returns>
        /// <remarks>This method also rearranges the bytes into SQL-Server's preferred byte order.</remarks>
        /// <see cref="http://blogs.msdn.com/b/dbrowne/archive/2012/07/03/how-to-generate-sequential-guids-for-sql-server-in-net.aspx" />
        private static Guid NewSequentialGuid()
        {
            Guid guid = UuidCreateSequential(out guid) == 0 ? guid : Guid.NewGuid();
            var @in = guid.ToByteArray();
            var @out = new byte[16];
            @out[3] = @in[0];
            @out[2] = @in[1];
            @out[1] = @in[2];
            @out[0] = @in[3];
            @out[5] = @in[4];
            @out[4] = @in[5];
            @out[7] = @in[6];
            @out[6] = @in[7];
            @out[8] = @in[8];
            @out[9] = @in[9];
            @out[10] = @in[10];
            @out[11] = @in[11];
            @out[12] = @in[12];
            @out[13] = @in[13];
            @out[14] = @in[14];
            @out[15] = @in[15];
            var result = new Guid(@out);

            return result;
        }

        [DllImport("rpcrt4.dll", SetLastError = true)]
        internal static extern int UuidCreateSequential(out Guid guid);
    }
}
