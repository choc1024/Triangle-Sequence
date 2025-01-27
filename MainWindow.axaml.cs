using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace test
{
    public partial class MainWindow : Window
    {
        private TextBox? _inputField;
        private Label?[]? _labels;

        public MainWindow()
        {
            InitializeComponent();

            // Assign elements to variables
            _inputField = this.FindControl<TextBox>("InputField");
            _labels = new[]
            {
                this.FindControl<Label>("Label1"),
                this.FindControl<Label>("Label2"),
                this.FindControl<Label>("Label3"),
                this.FindControl<Label>("Label4"),
                this.FindControl<Label>("Label5")
            };

            if (_inputField != null)
            {
                _inputField.KeyDown += OnKeyDown;
            }

            // Initialize labels as empty
            foreach (var label in _labels ?? Array.Empty<Label>())
            {
                if (label != null)
                {
                    label.Content = string.Empty;
                }
            }
        }

        private void OnKeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
        {
            // Check if Enter is pressed
            if (e.Key == Avalonia.Input.Key.Enter && _inputField != null && _labels != null)
            {
                // Trim whitespace from input field
                _inputField.Text = _inputField.Text?.Replace(" ", string.Empty);

                var currentText = _inputField.Text;

                if (int.TryParse(currentText, out int n))
                {
                    // Perform the calculation
                    double k = (-1 + Math.Sqrt(1 + 8 * n)) / 2;

                    double? result1 = null, result5 = null;

                    // Calculation for the first label
                    if (_labels[0] != null)
                    {
                        double x1;

                        if (k % 1 == 0) // Check if k is a whole number
                        {
                            x1 = k - 1;
                        }
                        else
                        {
                            x1 = Math.Floor(k); // Truncate decimals
                        }

                        result1 = (x1 * (x1 + 1)) / 2;
                        _labels[0]!.Content = $"Previous term: {result1}";
                    }

                    // Calculation for the fifth label
                    if (_labels[4] != null)
                    {
                        double x5;

                        if (k % 1 == 0) // Check if k is a whole number
                        {
                            x5 = k + 1;
                        }
                        else
                        {
                            x5 = Math.Ceiling(k); // Round up to the next whole integer
                        }

                        result5 = (x5 * (x5 + 1)) / 2;
                        _labels[4]!.Content = $"Next term: {result5}";
                    }

                    // Calculate labels 2, 3, and 4 if both results are available
                    if (result1.HasValue && result5.HasValue)
                    {
                        double difference = result5.Value - result1.Value;

                        // Label 2: 38.2% of the difference
                        if (_labels[1] != null)
                        {
                            double result2 = result1.Value + difference * 0.382;
                            _labels[1]!.Content = $"38.2%: {result2}";
                        }

                        // Label 3: 50% of the difference
                        if (_labels[2] != null)
                        {
                            double result3 = result1.Value + difference * 0.5;
                            _labels[2]!.Content = $"50%: {result3}";
                        }

                        // Label 4: 61.8% of the difference
                        if (_labels[3] != null)
                        {
                            double result4 = result1.Value + difference * 0.618;
                            _labels[3]!.Content = $"61.8%: {result4}";
                        }
                    }
                }
                else
                {
                    // If input is not a valid integer, display an error message for all labels
                    foreach (var label in _labels)
                    {
                        if (label != null)
                        {
                            label.Content = "You must input an integer";
                        }
                    }
                }
            }
        }
    }
}
