using System;
using System.Collections.Generic;
using System.Text;

namespace CSFileWriter
{
    /// <summary>
    /// CS对象的基类
    /// </summary>
    public abstract class CSObject
    {
        public string restrain;
        public string type;
        public string name;
        public string comment;

        /// <summary>
        /// 
        /// </summary>
        public string GenerateCommentString()
        {
            if (string.IsNullOrEmpty(comment))
                return string.Empty;

            StringBuilder commentBuilder = new StringBuilder();
            string[] commentLines = comment.Split(new string[] { "/r/n" }, StringSplitOptions.None);
            commentBuilder.AppendLine("/// <summary>");
            for (int i = 0; i < commentLines.Length; ++i)
            {
                commentBuilder.Append("/// ");
                commentBuilder.AppendLine(commentLines[i]);
            }
            commentBuilder.AppendLine("/// <summary>");
            return commentBuilder.ToString();
        }
    }

}
