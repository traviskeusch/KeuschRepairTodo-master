using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Todo.Views
{
    public class MaskedBehavior : Behavior<Entry>
    {
        private string _mask = "";
        public string Mask
        {
            get => _mask;
            set
            {
                _mask = value;
                SetPositions();
            }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(bindable);
        }

        IDictionary<int, char> _positions;

        void SetPositions()
        {
            if (string.IsNullOrEmpty(Mask))
            {
                _positions = null;
                return;
            }

            var list = new Dictionary<int, char>();
            for (var i = 0; i < Mask.Length; i++)
                if (Mask[i] != 'X')
                    list.Add(i, Mask[i]);

            _positions = list;
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = sender as Entry;

            var text = entry.Text;

            if (string.IsNullOrWhiteSpace(text) || _positions == null)
                return;

            if (text.Length > _mask.Length) 
            {
                entry.Text = text.Remove(text.Length -1);
                return;
            }

            foreach (var position in _positions)
                if (text.Length >= position.Key + 1)
                {
                    var value = position.Value.ToString();
                    if (text.Substring(position.Key, 1) != value)
                        text = text.Insert(position.Key, value);
                }

            if (entry.Text != text)
                entry.Text = text;
        }
    }

    //public class EmailValidatorBehavior : Behavior
    //{
    //    const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
    //        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

    //    static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(NumberValidatorBehavior), false);

    //    public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

    //    public bool IsValid
    //    {
    //        get { return (bool)base.GetValue(IsValidProperty); }
    //        private set { base.SetValue(IsValidPropertyKey, value); }
    //    }

    //    //protected override void OnAttachedTo(Entry bindable)
    //    //{
    //    //    bindable.TextChanged += HandleTextChanged;
    //    //}

    //    void HandleTextChanged(object sender, TextChangedEventArgs e)
    //    {
    //        IsValid = (Regex.IsMatch(e.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
    //        ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
    //    }

    //    //protected override void OnDetachingFrom(Entry bindable)
    //    //{
    //    //    bindable.TextChanged -= HandleTextChanged;

    //    //}
    //}
}
