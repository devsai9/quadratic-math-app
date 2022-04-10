using System;
using System.Windows.Forms;
using static System.Math;
using System.Text.RegularExpressions;

namespace mathapp
{
    public partial class app : Form
    {
        int firstNumber, secondNumber, thirdNumber;
        public app()
        {
            InitializeComponent();
        }

        private void practice_input_TextChanged(object sender, EventArgs e) 
        {
            var a = solve_input_a.Text;
            var b = solve_input_b.Text;
            var c = solve_input_c.Text;
            var headerRegex = new Regex(@"[-]");
            if (!headerRegex.IsMatch(b))
            {
                b = "+ " + b;
            }
            else if (headerRegex.IsMatch(b)) 
            {
                var bTrim = b.Trim(new[] {'-'});
                b = "- " + bTrim;
            }

            if (!headerRegex.IsMatch(c)) 
            {
                c = "+ " + c;
            }
            else if (headerRegex.IsMatch(c))
            {
                var cTrim = c.Trim(new[] { '-' });
                c = "- " + cTrim;
            }

            if (string.IsNullOrWhiteSpace(solve_input_a.Text) && string.IsNullOrWhiteSpace(solve_input_b.Text) && string.IsNullOrWhiteSpace(solve_input_c.Text))
            {
                solve_header.Text = "Enter a, b, and c values";
            }
            else if (string.IsNullOrWhiteSpace(solve_input_a.Text)) 
            {
                solve_header.Text = "?x² " + b + "x " + c;
                if (string.IsNullOrWhiteSpace(solve_input_b.Text) && !string.IsNullOrWhiteSpace(solve_input_c.Text))
                {
                    solve_header.Text = "?x² " + "+ " + "?x " + c;
                }

                if (string.IsNullOrWhiteSpace(solve_input_c.Text)) 
                {
                    solve_header.Text = "?x² " + b + "x " + "+ " + "?";
                }
            } 
            else if (string.IsNullOrWhiteSpace(solve_input_b.Text)) 
            {
                solve_header.Text = a + "x² " + "+ " + "?x " + c;

                if (string.IsNullOrWhiteSpace(solve_input_a.Text))
                {
                    solve_header.Text = "?x² " + "+ " + "?x " + c;
                }

                if (string.IsNullOrWhiteSpace(solve_input_c.Text))
                {
                    solve_header.Text = "?x² " + b + "x " + "+ " + "?";

                    if (string.IsNullOrWhiteSpace(solve_input_b.Text))
                    {
                        solve_header.Text = a + "x² " + "+ " + "?x " + "+ " + "?";
                    }
                }
            }
            else
            {
                solve_header.Text = a + "x² " + b + "x " + c;
            }
        }

        private void solve_btn_Click(object sender, EventArgs e)
        {
            var input_a = solve_input_a.Text;
            //int input_a_conv;
            var input_b = solve_input_b.Text;
            //int input_b_conv;
            var input_c = solve_input_c.Text;
            //int input_c_conv;

            //int.TryParse(input_a, out input_a_conv);
            //int.TryParse(input_b, out input_b_conv);
            //int.TryParse(input_c, out input_c_conv);
            var solveRegex = new Regex(@"[A-z]");
            if (solveRegex.IsMatch(input_a) || solveRegex.IsMatch(input_b) || solveRegex.IsMatch(input_c))
            {
                solve_output_1.Text = "Error";
                solve_output_2.Text = "Error";
                //return;
            }
            else if (string.IsNullOrEmpty(input_a) || string.IsNullOrEmpty(input_b) || string.IsNullOrEmpty(input_c))
            {
                solve_output_1.Text = "Error";
                solve_output_2.Text = "Error";
                //return;
            }
            else if (double.Parse(input_a) != double.NaN && double.Parse(input_b) != double.NaN && double.Parse(input_c) != double.NaN)
            {
                var quad = quadratic(int.Parse(input_a), int.Parse(input_b), int.Parse(input_c));
                solve_output_1.Text = quad.addResult.ToString();
                solve_output_2.Text = quad.subtractResult.ToString();
                if (quad.addResult.ToString() == "NaN" && quad.subtractResult.ToString() == "NaN") 
                {
                    solve_output_1.Text = "No solutions (ns)";
                    solve_output_2.Text = "No solutions (ns)";
                }
            }



            //var quadraticRegex = new Regex(@"\d*[a-z]{1}\^2[+-]\d*[a-z](?(?=[+\-])\d+)[=]?\d*");

            //if (quadraticRegex.IsMatch(input))
            //    solve_output.Text = "Placeholder: Pass";
            //else
            //    solve_output.Text = "Error";

            //if (!input.Contains("^2"))
            //{
            //    solve_output.Text = "Error";
            //    return;
            //}
            //else if (!input.Contains("x"))
            //{
            //    solve_output.Text = "Error";
            //    return;
            //}
            //else if (!input.Contains("+") && !input.Contains("-"))
            //{
            //    solve_output.Text = "Error";
            //    return;
            //}
            //else if (input.Length < 5) {
            //    solve_output.Text = "Error";
            //    return;
            //}

            //solve_output.Text = "Placeholder: Pass";


        }

        private void practice_gen_Click(object sender, EventArgs e)
        {
            firstNumber = generateRandomNumber(-10, 20);
            if (firstNumber == 0) {
                firstNumber = generateRandomNumber(10, 20);
            }
            secondNumber = generateRandomNumber(-15, 25);
            if (secondNumber == 0) {
                secondNumber = generateRandomNumber(15, 25);
            }
            thirdNumber = generateRandomNumber(-10, 2);
            //int firstOperator = generateRandomNumber(0, 1);
            //int secondOperator = generateRandomNumber(1, 2);

            var equation = firstNumber + "x^2 " + (secondNumber >= 0 ? "+ " : "") + secondNumber + "x " + (thirdNumber >= 0 ? "+ " : "") + thirdNumber;
            practice_eqdisplay.Text = equation;

            practice_input1.ReadOnly = false;
            practice_input1.Text = "";
            practice_input2.ReadOnly = false;
            practice_input2.Text = "";
            practice_inputsubmit.Enabled = true;

            practice_correctans_1.Text = "N/A";
            practice_correctans_1.BackColor = System.Drawing.Color.Transparent;
            practice_correctans_1.ForeColor = System.Drawing.Color.Black;
            practice_status.Text = "Unchecked";
            practice_status.ForeColor = System.Drawing.Color.Red;

            practice_input1_status.Text = "❌";
            practice_input2_status.Text = "❌";

            practice_input1_status.Visible = false;
            practice_input2_status.Visible = false;
        }

        private void practice_inputsubmit_Click(object sender, EventArgs e)
        {
            int addAnsPos = 0;

            practice_input1.ReadOnly = true;
            practice_input2.ReadOnly = true;
            practice_inputsubmit.Enabled = false;

            practice_input1_status.Visible = true;
            practice_input2_status.Visible = true;

            var quad = quadratic(firstNumber, secondNumber, thirdNumber);
            practice_correctans_1.Text = Math.Round(quad.addResult, 3).ToString();
            practice_correctans_1.BackColor = System.Drawing.Color.Black;
            practice_correctans_1.ForeColor = System.Drawing.Color.White;

            practice_correctans_2.Text = Math.Round(quad.subtractResult, 3).ToString();
            practice_correctans_2.BackColor = System.Drawing.Color.Black;
            practice_correctans_2.ForeColor = System.Drawing.Color.White;

            if (quad.addResult.ToString() == "NaN" && quad.subtractResult.ToString() == "NaN")
            {
                practice_correctans_1.Text = "No solutions (ns)";
                practice_correctans_1.BackColor = System.Drawing.Color.Black;
                practice_correctans_1.ForeColor = System.Drawing.Color.White;

                practice_correctans_2.Text = "No solutions (ns)";
                practice_correctans_2.BackColor = System.Drawing.Color.Black;
                practice_correctans_2.ForeColor = System.Drawing.Color.White;
            }

            practice_status.Text = "Checked";
            practice_status.ForeColor = System.Drawing.Color.Green;

            if (practice_input1.Text == Math.Round(quad.addResult, 3).ToString())
            {
                addAnsPos = 1;
            }
            else if (practice_input1.Text == Math.Round(quad.subtractResult, 3).ToString())
            {
                addAnsPos = 2;
            }
            else 
            {
                addAnsPos = 0;
            }

            if (practice_input1.Text == Math.Round(quad.addResult, 3).ToString())
            {
                if (addAnsPos == 1)
                {
                    practice_input1_status.Text = "✅";
                    practice_input1_status.ForeColor = System.Drawing.Color.Green;
                }
                else if (addAnsPos == 2 || addAnsPos == 0)
                {
                    practice_input1_status.Text = "❌";
                    practice_input1_status.ForeColor = System.Drawing.Color.Red;
                }
            }
            else if (practice_input1.Text == Math.Round(quad.subtractResult, 3).ToString())
            {
                if (addAnsPos == 1 || addAnsPos == 0)
                {
                    practice_input1_status.Text = "❌";
                    practice_input1_status.ForeColor = System.Drawing.Color.Red;
                }
                else if (addAnsPos == 2)
                {
                    practice_input1_status.Text = "✅";
                    practice_input1_status.ForeColor = System.Drawing.Color.Green;
                }
            }
            else 
            {
                practice_input1_status.Text = "❌";
                practice_input1_status.ForeColor = System.Drawing.Color.Red;
            }

            if (practice_input2.Text == Math.Round(quad.addResult, 3).ToString())
            {
                if (addAnsPos == 1)
                {
                    practice_input2_status.Text = "❌";
                    practice_input2_status.ForeColor = System.Drawing.Color.Red;
                }
                else if (addAnsPos == 2 || addAnsPos == 0)
                {
                    practice_input2_status.Text = "✅";
                    practice_input2_status.ForeColor = System.Drawing.Color.Green;
                }
            }
            else if (practice_input2.Text == Math.Round(quad.subtractResult, 3).ToString())
            {
                if (addAnsPos == 1 || addAnsPos == 0)
                {
                    practice_input2_status.Text = "✅";
                    practice_input2_status.ForeColor = System.Drawing.Color.Green;
                }
                else if (addAnsPos == 2)
                {
                    practice_input2_status.Text = "❌";
                    practice_input2_status.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                practice_input2_status.Text = "❌";
                practice_input2_status.ForeColor = System.Drawing.Color.Red;
            }

            if (quad.addResult.ToString() == "NaN" && quad.subtractResult.ToString() == "NaN")
            {
                if (practice_input1.Text == "ns")
                {
                    practice_input1_status.Text = "✅";
                    practice_input1_status.ForeColor = System.Drawing.Color.Green;
                }
                else if (practice_input2.Text == "ns") 
                {
                    practice_input2_status.Text = "✅";
                    practice_input2_status.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    practice_input1_status.Text = "❌";
                    practice_input1_status.ForeColor = System.Drawing.Color.Red;
                    practice_input2_status.Text = "❌";
                    practice_input2_status.ForeColor = System.Drawing.Color.Red;
                }

                if (practice_input1.Text.ToLower() == "no solution" || practice_input1.Text.ToLower() == "no solutions")
                {
                    practice_input1_status.Text = "✅";
                    practice_input1_status.ForeColor = System.Drawing.Color.Green;
                }
                if (practice_input2.Text.ToLower() == "no solution" || practice_input2.Text.ToLower() == "no solutions")
                {
                    practice_input2_status.Text = "✅";
                    practice_input2_status.ForeColor = System.Drawing.Color.Green;
                }
            }
        }

        private Quadratic quadratic(int a, int b, int c)
        {
            //var quadsqrtpart = Math.Pow(b, 2) - 4 * a * c;
            //var quadsqrt = quadsqrtpart > 0 ? Math.Sqrt(quadsqrtpart) : -1 * Math.Sqrt(-1 * quadsqrtpart);

            // Add
            var addx = ( -b + Math.Sqrt( Math.Pow(b, 2) - (4 * a * c) ) ) / (2 * a);

            // Subtract
            var subtractx = ( -b - Math.Sqrt( Math.Pow(b, 2) - (4 * a * c) ) ) / (2 * a);

            //if (addx == 0 && subtractx == 0)
            //{
            //    straddx = "0";
            //    strsubtractx = "0";
            //}


            Quadratic quadratic = new Quadratic();
            quadratic.addResult = addx;
            quadratic.subtractResult = subtractx;

            return quadratic;
        }

        //private double addQuadratic(int a, int b, int c)
        //{
        //    var result = (-b + System.Math.Sqrt(b ^ 2 - 4 * a * c)) / (2 * a);

        //    return result;
        //}
        //private double subtractQuadratic(int a, int b, int c)
        //{
        //    var result = (-b - System.Math.Sqrt(b ^ 2 - 4 * a * c)) / (2 * a);

        //    return result;
        //}
        private int generateRandomNumber(int min, int max)
        {
            Random rnd = new Random();
            //int num = rnd.Next(min, max);

            return rnd.Next(min, max);
        }
    }
}
