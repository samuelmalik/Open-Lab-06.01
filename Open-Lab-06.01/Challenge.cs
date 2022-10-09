using System;

namespace Open_Lab_06._01
{
    public class Challenge
    {
        public string MysteryFunc(string str)
        {
            string result = "";

            for (int i = 0; i < str.Length; i = i+2)
            {
                    for (int x = 0; x < Int32.Parse(str[i+1].ToString()); x++)
                    {
                        result += str[i];
                    }    
            }

            return result;
        }
    }
}
