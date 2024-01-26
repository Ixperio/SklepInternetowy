using Sklep.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection.Emit;

namespace Sklep.Prototype
{
    // Prototyp komentarza - Katarzyna Grygo
    public class CommentPrototype
    {
        public string UserName;
        public string Content;

        public CommentPrototype ShallowCopy()
        {
            return (CommentPrototype)MemberwiseClone();
        }

        public CommentPrototype DeepCopy()
        {
            CommentPrototype comment = (CommentPrototype)MemberwiseClone();
            comment.Content = string.Copy(Content);
            comment.UserName = string.Copy(UserName);
            return comment;
        }
    }
}