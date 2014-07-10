using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace CustomSyntaxClassifier
{
    #region Format definition
    /// <summary>
    /// Defines an editor format for the CustomSyntaxClassifier type that has a purple background
    /// and is underlined.
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "CustomSyntaxClassifier")]
    [Name("CustomSyntaxClassifier")]
    [UserVisible(true)] //this should be visible to the end user
    [Order(Before = Priority.Low)] //set the priority to be after the default classifiers
    internal sealed class CustomSyntaxClassifierFormat : ClassificationFormatDefinition
    {
        /// <summary>
        /// Defines the visual format for the "CustomSyntaxClassifier" classification type
        /// </summary>
        public CustomSyntaxClassifierFormat()
        {
            this.DisplayName = "Disposable Types"; //human readable version of the name
            this.ForegroundColor = Colors.Red;            
        }
    }
    #endregion //Format definition
}
