using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.CommonLibrary
{
    public record Employee : INotifyPropertyChanged
    {
        private string _name = "";
        private string _position = "";
        private string? _phone;
        private string _email = "";
        private string? _room;
        private string? _mainWorkplace;
        private string? _workplace;
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Name of the employee
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(_name);
                }

            }
        }
        /// <summary>
        /// Position of the employee
        /// </summary>
        public string Position
        {
            get => _position;
            set
            {
                if (_position != value)
                {
                    _position = value;
                    OnPropertyChanged(_position);
                }

            }
        }

        /// <summary>
        /// Phone number of the employee
        /// </summary>
        public string? Phone
        {
            get => _phone;
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    OnPropertyChanged(_phone);
                }
            }
        }

        /// <summary>
        /// Email of the employee
        /// </summary>
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(_email);
                }

            }
        }

        /// <summary>
        /// Room of the employee
        /// </summary>
        public string? Room
        {
            get => _room;
            set
            {
                if (_room != value)
                {
                    _room = value;
                    OnPropertyChanged(_room);
                }
            }
        }

        /// <summary>
        /// Main workplace of the employee
        /// </summary>
        public string? MainWorkplace
        {
            get { return _mainWorkplace; }
            set
            {
                if (_mainWorkplace != value)
                {
                    _mainWorkplace = value;
                    OnPropertyChanged(_mainWorkplace);
                }

            }
        }

        /// <summary>
        /// Workplace of the employee
        /// </summary>
        public string? Workplace
        {
            get => _workplace;
            set
            {
                if (_workplace != value)
                {
                    _workplace = value;
                    OnPropertyChanged(_workplace);
                }
            }
        }

    }
}