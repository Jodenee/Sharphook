using System;
using System.Collections.Generic;
using Sharphook.DataTypes;
using Sharphook.Models.ResponseObjects;

namespace Sharphook.Models
{
    public class Embed
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public DateTime? Timestamp { get; set; }
        public SharphookColor? Color { get; set; }
        public EmbedFooter? Footer { get; set; }
        public EmbedImage? Image { get; set; }
        public EmbedThumbnail? Thumbnail { get; set; }
        public EmbedAuthor? Author { get; set; }
        public List<EmbedField> Fields { get; set; }


        public Embed(string? embedTitle = null, string? embedDescription = null, string? embedUrl = null) 
        {
            Title = embedTitle;
            Description = embedDescription;
            Url = embedUrl;
            Fields = new List<EmbedField>();
        }

        public Embed(EmbedObject embedObject) 
        {
            Title = embedObject.title;
            Description = embedObject.description;
            Url = embedObject.url;
            Fields = new List<EmbedField>();

            if (embedObject.color != null)
            {
                Color = new SharphookColor((uint)embedObject.color);
            }

            if (embedObject.timestamp != null)
            {
                Timestamp = DateTime.Parse(embedObject.timestamp);
            }

            if (embedObject.footer != null)
            {
                Footer = new EmbedFooter(embedObject.footer.text, embedObject.footer.icon_url);
            }

            if (embedObject.image != null)
            {
                Image = new EmbedImage(embedObject.image.url);
            }

            if (embedObject.thumbnail != null)
            {
                Thumbnail = new EmbedThumbnail(embedObject.thumbnail.url);
            }

            if (embedObject.author != null)
            {
                Author = new EmbedAuthor(embedObject.author.name, embedObject.author.url, embedObject.author.icon_url);
            }

            if (embedObject.fields != null)
            {
                foreach (EmbedObjectField fieldObject in embedObject.fields)
                {
                    Fields.Add(new EmbedField(fieldObject.name, fieldObject.value, fieldObject.inline)); //problem was probably solved check for me tytyt
                    Console.WriteLine(fieldObject.inline);
                }
            }
        }

        public void SetTimestampFromDatetime(DateTime? customTimestamp)
        {
            Timestamp = customTimestamp ?? DateTime.Now;
        }

        public int GetTotalCharacters()
        {
            int totalCharacters = 0;

            if (Title != null) { totalCharacters += Title.Length; }
            if (Description != null) { totalCharacters += Description.Length; }
            if (Footer != null) { totalCharacters += Footer.Text.Length; }
            if (Author != null) { totalCharacters += Author.Name.Length; }

            foreach (EmbedField embedField in Fields)
            {
                totalCharacters += (embedField.Name.Length + embedField.Value.Length); 
            }

            return totalCharacters;
        }
    }

    public class EmbedFooter
    {
        public string Text;
        public string? IconUrl;

        public EmbedFooter(string footerText, string? footerIconUrl = null)
        {
            Text = footerText;
            IconUrl = footerIconUrl;
        }
    }
    public class EmbedImage 
    {
        public string Url;

        public EmbedImage(string imageUrl) 
        {
            Url = imageUrl;
        }
    }
    public class EmbedThumbnail 
    {
        public string Url;

        public EmbedThumbnail(string thumbnailUrl)
        {
            Url = thumbnailUrl;
        }
    }
    public class EmbedAuthor
    {
        public string Name;
        public string? Url;
        public string? IconUrl;

        public EmbedAuthor(string authorName, string? authorUrl = null, string? authorIconUrl = null)
        {
            Name = authorName;
            Url = authorUrl;
            IconUrl = authorIconUrl;
        }
    }
    public class EmbedField
    {
        public string Name;
        public string Value;
        public bool? InLine;

        public EmbedField(string fieldName, string fieldValue, bool? fieldInLine = null) 
        {
            Name = fieldName;
            Value = fieldValue;
            InLine = fieldInLine;
        }
    }
}
