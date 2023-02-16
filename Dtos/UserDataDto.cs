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
namespace Api.Dtos;

public class UserDataDto
{
    public UserDataDto(int id, string username, string? bio, int postCount, string? pFP)
    {
        Id = id;
        Username = username;
        Bio = bio;
        PostCount = postCount;
        PFP = pFP;
    }

    public int Id {get;set;}
    public string Username {get;set;}
    public string? Bio {get;set;}
    public int PostCount {get;set;}
    public string? PFP {get;set;}
}