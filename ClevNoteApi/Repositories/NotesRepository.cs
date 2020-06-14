using Fiszki.Controllers;
using Fiszki.Services;
using Fiszki.VievModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fiszki.Repositories
{
    public class NotesRepository
    {

        public static List<NotesModel> Get(string itemType, int userId)
        {
            using (var dbContext = new FiszkiContext())
                if (String.IsNullOrEmpty(itemType))
                {
                    return dbContext.NotesTable.Where(item => item.userId == userId).ToList();
                }
                else
                {
                    return dbContext.NotesTable.Where(item => item.type == itemType && item.userId == userId).ToList();
                }
        }


        public static List<NotesModel> GetNotesId(int id, int Userid)
        {
            using (var dbContext = new FiszkiContext())
            {
                return dbContext.NotesTable.Where(item => item.id == id && item.userId == Userid).ToList();

            }
        }


        public static List<NotesModel> AddNote(NotesModel NewNote, int Userid)
        {
            using (var dbContext = new FiszkiContext())
            {
                var newNote = new NotesModel
                {
                    userId = Userid,
                    type = NewNote.type,
                    title = NewNote.title,
                    articleUrl = NewNote.articleUrl,
                    noteContent = NewNote.noteContent,
                    dateUser = NewNote.dateUser,
                    created = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    lastEdit = null,
                    rating = NewNote.rating,
                    isActive = 1,
                };
                dbContext.NotesTable.Add(newNote);
                dbContext.SaveChanges();

                int id = newNote.id;
                return dbContext.NotesTable.Where(item => item.id == id).ToList();
            }
        }


        public static List<NotesModel> EditNote(int id, NotesModel Note, int Userid)
        {
            using (var dbContext = new FiszkiContext())
            {
                var editedNote = dbContext.NotesTable.FirstOrDefault(x => x.id == id && x.userId == Userid);
                {
                    editedNote.type = Note.type;
                    editedNote.title = Note.title;
                    editedNote.articleUrl = Note.articleUrl;
                    editedNote.noteContent = Note.noteContent;
                    editedNote.dateUser = Note.dateUser;
                    editedNote.lastEdit = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    editedNote.rating = Note.rating;
                    editedNote.isActive = 1;
                };
                dbContext.SaveChanges();
                var EditetItem = dbContext.NotesTable.Where(item => item.id == id).ToList();
                return EditetItem;
            }
        }



        public static int DeleteNote(int id, int Userid)
        {
            using (var dbContext = new FiszkiContext())
            {
                var note = dbContext.NotesTable.FirstOrDefault(x => x.id == id && x.userId == Userid);
                if (note == null)
                {
                    return 0;
                }
                dbContext.NotesTable.Remove(note);
                dbContext.SaveChanges();
                return id;
            }
        }


    }
}
