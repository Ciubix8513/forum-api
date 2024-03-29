//The GPLv3 License (GPLv3)
//
//Copyright (c) 2023 Ciubix8513
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
namespace Api.Data.Entities;

public class Post
{
    public Post(int id, int? creatorId, int? parentPostId, string? contents, DateTime? date)
    {
        Id = id;
        CreatorId = creatorId;
        ParentPostId = parentPostId;
        Contents = contents;
        Date = date;
    }

    public int Id {get; set;}
    public int? CreatorId {get; set;}
    public int? ParentPostId {get; set;}
    public string? Contents {get; set;}
    public DateTime? Date {get; set;}
}