using System;
using System.Collections.Generic;
using System.Text;

static class Lexer
{
    public static List<String> SplitExpression(this String line)
    {
        List<String> result = new List<String>();
        StringBuilder buffer = new StringBuilder();
        foreach (Char i in line)
        {
            switch (i)
            {
                case ' ':
                case '\n':
                    continue;
                case '+':
                case '-':
                case '*':
                case '/':
                case '^':
                case '(':
                case ')':
                    if (buffer.Length > 0)
                    {
                        result.Add(buffer.ToString());
                        buffer.Clear();
                    }
                    result.Add(i.ToString());
                    continue;
            }
            buffer.Append(i);
        }
        if (buffer.Length > 0)
        {
            result.Add(buffer.ToString());
        }
        return result;
    }
}
