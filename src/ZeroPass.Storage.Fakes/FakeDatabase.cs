﻿using System.Collections.Generic;
using System.Text.Json;
using ZeroPass.Storage.Entities;

namespace ZeroPass.Storage.Fakes
{
    public class FakeDatabase
    {
        int GlobalUserId = 1;
        public readonly List<UserEntity> Users = new List<UserEntity>();
        public int AllocateUserId()
            => GlobalUserId++;

        int GlobalDomainId = 1;
        public readonly List<DomainUserEntity> DomainUsers = new List<DomainUserEntity>();
        public int AllocateDomainId()
            => GlobalDomainId++;

        public readonly List<UserProfileEntity> UserProfiles = new List<UserProfileEntity>();

        public readonly List<UserKeyEntity> UserKeys = new List<UserKeyEntity>();

        public readonly List<DomainEntity> Domains = new List<DomainEntity>();

        public readonly List<DomainInfoEntity> DomainInfos = new List<DomainInfoEntity>();

        public readonly List<TestUserSecret> UserSecrets = new List<TestUserSecret>();
        
        public readonly List<UserKeyDistributionEntity> UserKeyDistribution = new List<UserKeyDistributionEntity>();
        
        int GlobalNoticationId = 1;
        public readonly List<NotificationEntity<JsonElement, JsonElement>> Notifications = new List<NotificationEntity<JsonElement, JsonElement>>();
        public int AllocateNotificationId()
            => GlobalNoticationId++;
    }
}
