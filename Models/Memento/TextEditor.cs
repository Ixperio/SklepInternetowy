using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Sklep.Models.Memento
{
    public class TextEditor
    {
        private StringBuilder text = new StringBuilder();
        private Stack<Memento> undoStack = new Stack<Memento>();
        private Stack<Memento> redoStack = new Stack<Memento>();

        public string Text
        {
            get { return text.ToString(); }
            set
            {
                text.Clear();
                text.Append(value);
            }
        }

        // Dodaje literkę do tekstu i zapisuje stan
        public void AddCharacter(char character)
        {
            text.Append(character);
            SaveState();
            redoStack.Clear(); // Po dodaniu nowej literki, czyszczenie redoStack
        }

        // Zapisuje stan w obiekcie Memento
        private void SaveState()
        {
            undoStack.Push(new Memento(text.ToString()));
        }

        // Przywraca poprzedni stan
        public void Undo()
        {
            if (undoStack.Count > 1)
            {
                redoStack.Push(undoStack.Pop());
                text = new StringBuilder(undoStack.Peek().State);

            }
        }

        // Przywraca wcześniej cofnięty stan
        public void Redo()
        {
            if (redoStack.Count > 0)
            {
                undoStack.Push(redoStack.Pop());
                text = new StringBuilder(undoStack.Peek().State);
            }
        }
    }
}