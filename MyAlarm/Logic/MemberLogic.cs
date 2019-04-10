using Microsoft.EntityFrameworkCore;
using MyAlarm.Domain;
using MyAlarm.EFStandard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class MemberLogic : BaseLogic
    {
        public MemberLogic(string connectionString) : base(connectionString)
        {

        }
        public MemberLogic(dataContext dbContext) : base(dbContext)
        {

        }
        public IQueryable<Member> GetAllAsync()
        {

            IQueryable<Member> query = _DbContext.Member;
            query = query.AsNoTracking().OrderBy(h => h.Name);
            return query;
        }
        
        public async Task<Member> CreateMember(Member member)
        {
            
                var item = new Member
                {
                    Id = member.Id,
                    Email = member.Email,
                    FkRole = member.FkRole,
                    Gender = member.Gender,
                    Name = member.Name,
                    NumPhone = member.NumPhone
                };
                _DbContext.Member.Add(item);
                try
                {
                    await _DbContext.SaveChangesAsync().ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    throw e;
                }
                return item;
                       
        }

        public async Task<bool> DeleteMember(string idMember)
        {
            try
            {
                var item = await _DbContext.Member.FirstOrDefaultAsync(h => h.Id == idMember).ConfigureAwait(false);
                if (item == null)
                {
                    return false;
                }
                //xóa các bảng liên quan
                _DbContext.Member.Remove(item);
                await _DbContext.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        public async Task<bool> Login(string email, string pass)
        {
            try
            {
                var isLogin = await _DbContext.Member.FirstOrDefaultAsync(h => h.Email == email && h.Password == pass);
                if (isLogin == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public async Task<Member> GetMember(string email)
        {
            return await _DbContext.Member.FirstOrDefaultAsync(h => h.Email == email);
        }

        public async Task<Member> EditMember(Member member)
        {
            try
            {
                var item = await _DbContext.Member.FirstOrDefaultAsync(h => h.Id == member.Id);
                item.Email = member.Email;
                item.Name = member.Name;
                item.NumPhone = member.NumPhone;
                await _DbContext.SaveChangesAsync().ConfigureAwait(false);
                return item;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> ChangePass(Member member)
        {
            try
            {
                var item = await _DbContext.Member.FirstOrDefaultAsync(h => h.Id == member.Id);
                item.Password = member.Password;
                await _DbContext.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
