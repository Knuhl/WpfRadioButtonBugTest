using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace WpfRadioButtonBugTest
{
    public class TestViewModel : PropertyChangedBase
    {
        public ObservableCollection<Element> Elements { get; }
            = new ObservableCollection<Element>(Enumerable.Range(0, 2).Select(x => new Element(x + 1)));

        private Element _selectedElement;
        public Element SelectedElement
        {
            get => _selectedElement ?? (_selectedElement = Elements.First());
            set
            {
                _selectedElement = value;
                OnPropertyChanged();
            }
        }
    }

    public class Element : PropertyChangedBase
    {
        private bool _memberA = true;
        public bool MemberA
        {
            get => _memberA;
            set
            {
                _memberA = value;
                OnPropertyChanged();
            }
        }

        private bool _memberB;
        public bool MemberB
        {
            get => _memberB;
            set
            {
                _memberB = value;
                OnPropertyChanged();
            }
        }

        public string GroupName { get; }

        public Element(int i) => GroupName = i.ToString();
    }

    public class PropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
