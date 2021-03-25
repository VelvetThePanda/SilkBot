﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Silk.Core.Data.Models;

namespace Silk.Core.Data.MediatR.Handlers
{
    public class UserHandler
    {
        public class GetHandler : IRequestHandler<UserRequest.Get, User?>
        {
            private readonly GuildContext _db;
            public GetHandler(GuildContext db) => _db = db;

            public async Task<User?> Handle(UserRequest.Get request, CancellationToken cancellationToken)
            {
                User? user = await _db.Users
                    .FirstOrDefaultAsync(u => u.Id == request.UserId &&
                                              u.GuildId == request.GuildId, cancellationToken);
                return user;
            }
        }

        public class AddHandler : IRequestHandler<UserRequest.Add, User>
        {
            private readonly GuildContext _db;

            public AddHandler(GuildContext db) => _db = db;

            public async Task<User> Handle(UserRequest.Add request, CancellationToken cancellationToken)
            {
                var user = new User {Id = request.UserId, GuildId = request.GuildId, Flags = request.Flags ?? UserFlag.None};
                _db.Users.Add(user);
                await _db.SaveChangesAsync(cancellationToken);
                return user;
            }
        }

        public class UpdateHandler : IRequestHandler<UserRequest.Update, User>
        {
            private readonly GuildContext _db;

            public UpdateHandler(GuildContext db)
            {
                _db = db;
            }

            public async Task<User> Handle(UserRequest.Update request, CancellationToken cancellationToken)
            {
                User user = await _db.Users.FirstAsync(u => u.Id == request.UserId && u.GuildId == request.GuildId, cancellationToken);

                user.Flags = request.Flags ?? user.Flags;
                await _db.SaveChangesAsync(cancellationToken);

                return user;
            }
        }

        public class GetOrCreateHandler : IRequestHandler<UserRequest.GetOrCreate, User>
        {
            private readonly GuildContext _db;
            public GetOrCreateHandler(GuildContext db) => _db = db;

            public async Task<User> Handle(UserRequest.GetOrCreate request, CancellationToken cancellationToken)
            {
                User? user =
                    await _db.Users
                        .Include(u => u.Guild)
                        .FirstOrDefaultAsync(u => u.Id == request.UserId && u.GuildId == request.GuildId, cancellationToken);

                if ((User?) user is null)
                {
                    Guild guild = await _db.Guilds.FirstAsync(g => g.Id == request.GuildId, cancellationToken);
                    user = new()
                    {
                        GuildId = request.GuildId,
                        Id = request.UserId,
                        Flags = request.Flags ?? UserFlag.None,
                    };
                    guild.Users.Add(user);
                    await _db.SaveChangesAsync(cancellationToken);
                }

                return user;
            }
        }
    }
}