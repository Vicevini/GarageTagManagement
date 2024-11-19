using System;
using System.Collections.Generic;
using GarageTagManagement.Models;
using GarageTagManagement.Repositories;

namespace GarageTagManagement.Services
{
    public class TagAddedEventArgs : EventArgs
    {
        public int TagId { get; }
        public string IdApartamento { get; }
        public DateTime ValidadeTag { get; }

        public TagAddedEventArgs(int tagId, string apartmentId, DateTime validadeTag)
        {
            TagId = tagId;
            IdApartamento = apartmentId.ToString();
            ValidadeTag = validadeTag;
        }

    }

    public class TagUpdatedEventArgs : EventArgs
    {
        public int TagId { get; }
        public string IdApartamento { get; }
        public DateTime ValidadeTag { get; }

        public TagUpdatedEventArgs(int tagId, string apartmentId, DateTime validadeTag)
        {
            TagId = tagId;
            IdApartamento = apartmentId;
            ValidadeTag = validadeTag;
        }
    }

    public class TagDeletedEventArgs : EventArgs
    {
        public int TagId { get; }

        public TagDeletedEventArgs(int tagId)
        {
            TagId = tagId;
        }
    }

    public class NewApartamentRegisteredEventArgs : EventArgs
    {
        public string IdApartamento { get; }

        public NewApartamentRegisteredEventArgs(string idApartamento)
        {
            IdApartamento = idApartamento;
        }
    }

    public class NewTagCreatedEventArgs : EventArgs
    {
        public Tag Tag { get; }

        public NewTagCreatedEventArgs(Tag tag)
        {
            Tag = tag;
        }
    }

    public class NewVisitorTagCreatedEventArgs : EventArgs
    {
        public Tag Tag { get; }

        public NewVisitorTagCreatedEventArgs(Tag tag)
        {
            Tag = tag;
        }
    }

    public class TagDeactivatedEventArgs : EventArgs
    {
        public int TagId { get; }

        public TagDeactivatedEventArgs(int tagId)
        {
            TagId = tagId;
        }
    }

    public class VisitorTagDeactivatedEventArgs : EventArgs
    {
        public int TagId { get; }

        public VisitorTagDeactivatedEventArgs(int tagId)
        {
            TagId = tagId;
        }
    }

    public class TagValidadeExtendedEventArgs : EventArgs
    {
        public int TagId { get; }
        public DateTime ValidadeTag { get; }

        public TagValidadeExtendedEventArgs(int tagId, DateTime validadeTag)
        {
            TagId = tagId;
            ValidadeTag = validadeTag;
        }
    }

    public class VisitorTagValidadeExtendedEventArgs : EventArgs
    {
        public int TagId { get; }
        public DateTime ValidadeTag { get; }

        public VisitorTagValidadeExtendedEventArgs(int tagId, DateTime validadeTag)
        {
            TagId = tagId;
            ValidadeTag = validadeTag;
        }
    }

    public class TagValidadeExpiredEventArgs : EventArgs
    {
        public int TagId { get; }

        public TagValidadeExpiredEventArgs(int tagId)
        {
            TagId = tagId;
        }
    }

    public class VisitorTagValidadeExpiredEventArgs : EventArgs
    {
        public int TagId { get; }

        public VisitorTagValidadeExpiredEventArgs(int tagId)
        {
            TagId = tagId;
        }
    }
    

    public class TagService
    {
        private readonly TagRepository _tagRepository;

        public event EventHandler<TagAddedEventArgs>? TagAdded;

        public event EventHandler<TagUpdatedEventArgs>? TagUpdated;

        public event EventHandler<TagDeletedEventArgs>? TagDeleted;

        public event EventHandler<NewApartamentRegisteredEventArgs>? NewApartamentRegistered;

        public event EventHandler<NewTagCreatedEventArgs>? NewTagCreated;

        public event EventHandler<NewVisitorTagCreatedEventArgs>? NewVisitorTagCreated;

        public event EventHandler<TagDeactivatedEventArgs>? TagDeactivated;

        public event EventHandler<VisitorTagDeactivatedEventArgs>? VisitorTagDeactivated;

        public event EventHandler<TagValidadeExtendedEventArgs>? TagValidadeExtended;

        public event EventHandler<VisitorTagValidadeExtendedEventArgs>? VisitorTagValidadeExtended;

        public event EventHandler<TagValidadeExpiredEventArgs>? TagValidadeExpired;

        public event EventHandler<VisitorTagValidadeExpiredEventArgs>? VisitorTagValidadeExpired;

        public TagService(TagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public IEnumerable<Tag> GetAll()
        {
            return _tagRepository.GetAll();
        }

        public Tag GetById(int id)
        {
            return _tagRepository.GetById(id);
        }

        public Tag GetByApartment(int id)
        {
            return _tagRepository.GetByApartment(id);
        }

        public void Add(Tag tag)
        {
            if (tag.ValidadeTag == null)
            {
                tag.ValidadeTag = DateTime.Now.AddYears(1);
            }

            _tagRepository.Add(tag);

            if (tag.IdApartamento != null)
            {
                if (tag.IdApartamento != null)
                {
                    OnTagAdded(new TagAddedEventArgs(tag.Id, tag.IdApartamento?.ToString() ?? string.Empty, tag.ValidadeTag.Value));
                }
            }
        }

        public void Update(Tag tag)
        {
            _tagRepository.Update(tag);

            if (tag.IdApartamento != null)
            {
                if (tag.ValidadeTag.HasValue)
                {
                    if (tag.IdApartamento != null)
                    {
                        onTagUpdated(new TagUpdatedEventArgs(tag.Id, tag.IdApartamento?.ToString() ?? string.Empty, tag.ValidadeTag.Value));
                    }
                }
            }
        }

        public void Delete(int id)
        {
            _tagRepository.Delete(id);
            OnTagDeleted(new TagDeletedEventArgs(id));

            TagDeletedEventArgs e = new TagDeletedEventArgs(id);
        }

        public bool IsTagValid(Tag tag)
        {
            return tag.ValidadeTag >= DateTime.Now;
        }

        public bool IsTagValidById(int id)
        {
            var tag = _tagRepository.GetById(id);
            return IsTagValid(tag);
        }

        public bool IsTagValidByApartment(int id)
        {
            var tag = _tagRepository.GetByApartment(id);
            return IsTagValid(tag);
        }

        public void ToggleIsActive(int id)
        {
            var tag = _tagRepository.GetById(id);
            tag.IsActive = !tag.IsActive;
            _tagRepository.Update(tag);
        }

        public void ToggleValidade(int id, int hours)
        {
            var tag = _tagRepository.GetById(id);
            if (tag.ValidadeTag.HasValue)
            {
                tag.ValidadeTag = tag.ValidadeTag.Value.AddHours(hours);
            }
            _tagRepository.Update(tag);
        }

        protected virtual void OnTagAdded(TagAddedEventArgs e)
        {
            TagAdded?.Invoke(this, e);
        }

        protected virtual void onTagUpdated(TagUpdatedEventArgs e)
        {
            TagUpdated?.Invoke(this, e);
        }

        protected virtual void OnTagDeleted(TagDeletedEventArgs e)
        {
            TagDeleted?.Invoke(this, e);
        }

        protected virtual void OnNewApartamentRegistered(NewApartamentRegisteredEventArgs e)
        {
            NewApartamentRegistered?.Invoke(this, e);
        }

        protected virtual void OnNewTagCreated(NewTagCreatedEventArgs e)
        {
            NewTagCreated?.Invoke(this, e);
        }

        protected virtual void OnNewVisitorTagCreated(NewVisitorTagCreatedEventArgs e)
        {
            NewVisitorTagCreated?.Invoke(this, e);
        }

        protected virtual void OnTagDeactivated(TagDeactivatedEventArgs e)
        {
            TagDeactivated?.Invoke(this, e);
        }

        protected virtual void OnVisitorTagDeactivated(VisitorTagDeactivatedEventArgs e)
        {
            VisitorTagDeactivated?.Invoke(this, e);
        }

        protected virtual void OnTagValidadeExtended(TagValidadeExtendedEventArgs e)
        {
            TagValidadeExtended?.Invoke(this, e);
        }

        protected virtual void OnVisitorTagValidadeExtended(VisitorTagValidadeExtendedEventArgs e)
        {
            VisitorTagValidadeExtended?.Invoke(this, e);
        }

        protected virtual void OnTagValidadeExpired(TagValidadeExpiredEventArgs e)
        {
            TagValidadeExpired?.Invoke(this, e);
        }

        protected virtual void OnVisitorTagValidadeExpired(VisitorTagValidadeExpiredEventArgs e)
        {
            VisitorTagValidadeExpired?.Invoke(this, e);
        }
        
    }
}
