using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.Language.StandardClassification;
using System.Text.RegularExpressions;

namespace CustomSyntaxClassifier
{

    #region Provider definition
    /// <summary>
    /// This class causes a classifier to be added to the set of classifiers. Since 
    /// the content type is set to "text", this classifier applies to all text files
    /// </summary>
    [Export(typeof(IClassifierProvider))]
    [ContentType("text")]
    //[ContentType("code")] would like to limit it as much as possible
    internal class CustomSyntaxClassifierProvider : IClassifierProvider
    {
        /// <summary>
        /// Import the classification registry to be used for getting a reference
        /// to the custom classification type later.
        /// </summary>
        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry = null; // Set via MEF

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            return buffer.Properties.GetOrCreateSingletonProperty<CustomSyntaxClassifier>(delegate { return new CustomSyntaxClassifier(ClassificationRegistry); });
        }
    }
    #endregion //provider def

    #region Classifier
    /// <summary>
    /// Classifier that classifies all text as an instance of the OrinaryClassifierType
    /// </summary>
    class CustomSyntaxClassifier : IClassifier
    {
        IClassificationType _classificationType;
        IClassificationType _keywordType;

        internal CustomSyntaxClassifier(IClassificationTypeRegistryService registry)
        {
            _classificationType = registry.GetClassificationType("CustomSyntaxClassifier");
            _keywordType = registry.GetClassificationType(PredefinedClassificationTypeNames.Keyword);
        }

        /// <summary>
        /// This method scans the given SnapshotSpan for potential matches for this classification.
        /// In this instance, it classifies everything and returns each span as a new ClassificationSpan.
        /// </summary>
        /// <param name="trackingSpan">The span currently being classified</param>
        /// <returns>A list of ClassificationSpans that represent spans identified to be of this classification</returns>
        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            // just a test for one word for now
            Regex ex = new Regex("Stream");
            var matches = ex.Matches(span.GetText());

             List<ClassificationSpan> classifications = new List<ClassificationSpan>();
             foreach (Match match in matches)
             {
                 var newSnapshot = new SnapshotSpan(span.Snapshot, new Span(match.Index, match.Length));
                                                            
                 var newTest = newSnapshot.GetText();
                 classifications.Add(new ClassificationSpan(newSnapshot, _classificationType));;
             }

            return classifications;
        }

#pragma warning disable 67
        // This event gets raised if a non-text change would affect the classification in some way,
        // for example typing /* would cause the classification to change in C# without directly
        // affecting the span.
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
#pragma warning restore 67
    }
    #endregion //Classifier
}
