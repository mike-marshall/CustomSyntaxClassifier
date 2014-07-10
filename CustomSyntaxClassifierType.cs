using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace CustomSyntaxClassifier
{
    internal static class CustomSyntaxClassifierClassificationDefinition
    {
        /// <summary>
        /// Defines the "CustomSyntaxClassifier" classification type.
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("CustomSyntaxClassifier")]
        internal static ClassificationTypeDefinition CustomSyntaxClassifierType = null;
    }
}
