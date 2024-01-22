using Sklep.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection.Emit;

namespace Sklep.Prototype
{
    public class Comment
    {
        public string UserName;
        public string Content;

        public Comment ShallowCopy()
        {
            return (Comment)MemberwiseClone();
        }

        public Comment DeepCopy()
        {
            Comment comment = (Comment)MemberwiseClone();
            comment.Content = string.Copy(Content);
            comment.UserName = string.Copy(UserName);
            return comment;
        }
    }
}