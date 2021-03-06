USE [ReservationSystem]
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'1', N'Admin', N'ADMIN', N'895dd28c-2702-4095-aa32-e7a571de0321')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'2', N'Employee', N'EMPLOYEE', N'448c1c3a-9ded-4e91-bf92-6fef4fec5223')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'3', N'Member', N'MEMBER', N'cb2bbc11-90f6-4435-8796-09aa5a2f6af1')
GO
SET IDENTITY_INSERT [dbo].[ReservationOrigins] ON 

INSERT [dbo].[ReservationOrigins] ([Id], [Description]) VALUES (1, N'Online')
INSERT [dbo].[ReservationOrigins] ([Id], [Description]) VALUES (2, N'Phone')
INSERT [dbo].[ReservationOrigins] ([Id], [Description]) VALUES (3, N'Walk-in')
SET IDENTITY_INSERT [dbo].[ReservationOrigins] OFF
GO
SET IDENTITY_INSERT [dbo].[ReservationStatuses] ON 

INSERT [dbo].[ReservationStatuses] ([Id], [Description]) VALUES (1, N'Pending')
INSERT [dbo].[ReservationStatuses] ([Id], [Description]) VALUES (2, N'Confirmed')
INSERT [dbo].[ReservationStatuses] ([Id], [Description]) VALUES (3, N'Cancelled')
SET IDENTITY_INSERT [dbo].[ReservationStatuses] OFF
GO
SET IDENTITY_INSERT [dbo].[Restaurants] ON 

INSERT [dbo].[Restaurants] ([Id], [Name], [Address], [PhoneNumber], [DefaultCapacity]) VALUES (1, N'Bean Scene', N'12 Springfield rd', N'12345678', 100)
SET IDENTITY_INSERT [dbo].[Restaurants] OFF
GO
SET IDENTITY_INSERT [dbo].[People] ON 

INSERT [dbo].[People] ([Id], [FirstName], [LastName], [PhoneNumber], [Email], [UserId], [RestaurantId], [Discriminator], [EmployeeId]) VALUES (1, N'Damien', N'Antonietti', N'015723892', N'g@g.com', NULL, 1, N'Person', NULL)
INSERT [dbo].[People] ([Id], [FirstName], [LastName], [PhoneNumber], [Email], [UserId], [RestaurantId], [Discriminator], [EmployeeId]) VALUES (2, N'Kaleena', N'Byrne', N'023457123', N't@k.com', NULL, 1, N'Employee', 1)
INSERT [dbo].[People] ([Id], [FirstName], [LastName], [PhoneNumber], [Email], [UserId], [RestaurantId], [Discriminator], [EmployeeId]) VALUES (3, N'Kathleen', N'Smith', N'0298833412', N'ok@k.com', NULL, 1, N'Employee', 2)
INSERT [dbo].[People] ([Id], [FirstName], [LastName], [PhoneNumber], [Email], [UserId], [RestaurantId], [Discriminator], [EmployeeId]) VALUES (4, N'Andrew', N'Gardner', N'015656165', N'h@h.com', NULL, 1, N'Person', NULL)
INSERT [dbo].[People] ([Id], [FirstName], [LastName], [PhoneNumber], [Email], [UserId], [RestaurantId], [Discriminator], [EmployeeId]) VALUES (5, N'Emily', N'Smith', N'023462343', N'emilyd@d.com', NULL, 1, N'Customer', NULL)
INSERT [dbo].[People] ([Id], [FirstName], [LastName], [PhoneNumber], [Email], [UserId], [RestaurantId], [Discriminator], [EmployeeId]) VALUES (6, N'Frederique', N'Corbyn', N'0232341789', N'c.c@c.com', NULL, 1, N'Customer', NULL)
INSERT [dbo].[People] ([Id], [FirstName], [LastName], [PhoneNumber], [Email], [UserId], [RestaurantId], [Discriminator], [EmployeeId]) VALUES (7, N'Brendan', N'Chappell', N'015723832', N'j@j.com', NULL, 1, N'Person', NULL)
INSERT [dbo].[People] ([Id], [FirstName], [LastName], [PhoneNumber], [Email], [UserId], [RestaurantId], [Discriminator], [EmployeeId]) VALUES (8, N'John', N'Smith', N'3644253462', N'b.b@b.com', NULL, 1, N'Customer', NULL)
INSERT [dbo].[People] ([Id], [FirstName], [LastName], [PhoneNumber], [Email], [UserId], [RestaurantId], [Discriminator], [EmployeeId]) VALUES (9, N'Conor', N'O''Neill', N'015725832', N'k@k.com', NULL, 1, N'Person', NULL)
INSERT [dbo].[People] ([Id], [FirstName], [LastName], [PhoneNumber], [Email], [UserId], [RestaurantId], [Discriminator], [EmployeeId]) VALUES (10, N'Jim', N'Jones', N'023465123', N'pat@k.com', NULL, 1, N'Employee', 3)
INSERT [dbo].[People] ([Id], [FirstName], [LastName], [PhoneNumber], [Email], [UserId], [RestaurantId], [Discriminator], [EmployeeId]) VALUES (11, N'John', N'Doe', N'023465153', N'johndoe@k.com', NULL, 1, N'Employee', 4)
INSERT [dbo].[People] ([Id], [FirstName], [LastName], [PhoneNumber], [Email], [UserId], [RestaurantId], [Discriminator], [EmployeeId]) VALUES (12, N'William', N'Kemshell', N'023456789', N'a.a@a.com', NULL, 1, N'Customer', NULL)
SET IDENTITY_INSERT [dbo].[People] OFF
GO
SET IDENTITY_INSERT [dbo].[SittingTypes] ON 

INSERT [dbo].[SittingTypes] ([Id], [Description], [ResDuration]) VALUES (1, N'Breakfast', 45)
INSERT [dbo].[SittingTypes] ([Id], [Description], [ResDuration]) VALUES (2, N'Lunch', 60)
INSERT [dbo].[SittingTypes] ([Id], [Description], [ResDuration]) VALUES (3, N'Dinner', 90)
SET IDENTITY_INSERT [dbo].[SittingTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Sittings] ON 

INSERT [dbo].[Sittings] ([Id], [Title], [StartTime], [EndTime], [Capacity], [IsClosed], [ResDuration], [PeopleBooked], [RestaurantId], [SittingTypeId]) VALUES (1, N'Breakfast', CAST(N'2022-07-13T07:00:00.0000000' AS DateTime2), CAST(N'2022-07-13T11:30:00.0000000' AS DateTime2), 100, 0, 45, 3, 1, 1)
INSERT [dbo].[Sittings] ([Id], [Title], [StartTime], [EndTime], [Capacity], [IsClosed], [ResDuration], [PeopleBooked], [RestaurantId], [SittingTypeId]) VALUES (2, N'Lunch', CAST(N'2022-07-13T12:00:00.0000000' AS DateTime2), CAST(N'2022-07-13T15:30:00.0000000' AS DateTime2), 100, 0, 0, 4, 1, 2)
INSERT [dbo].[Sittings] ([Id], [Title], [StartTime], [EndTime], [Capacity], [IsClosed], [ResDuration], [PeopleBooked], [RestaurantId], [SittingTypeId]) VALUES (3, N'Dinner', CAST(N'2022-07-13T18:00:00.0000000' AS DateTime2), CAST(N'2022-07-13T21:30:00.0000000' AS DateTime2), 100, 0, 0, 5, 1, 3)
SET IDENTITY_INSERT [dbo].[Sittings] OFF
GO
SET IDENTITY_INSERT [dbo].[Reservations] ON 

INSERT [dbo].[Reservations] ([Id], [StartTime], [Guests], [Comments], [SittingId], [ReservationStatusId], [ReservationOriginId], [CustomerId]) VALUES (1, CAST(N'2022-07-13T09:30:00.0000000' AS DateTime2), 3, N'By the balcony, please.', 1, 1, 1, 5)
INSERT [dbo].[Reservations] ([Id], [StartTime], [Guests], [Comments], [SittingId], [ReservationStatusId], [ReservationOriginId], [CustomerId]) VALUES (2, CAST(N'2022-07-13T12:30:00.0000000' AS DateTime2), 4, N'', 2, 2, 2, 8)
INSERT [dbo].[Reservations] ([Id], [StartTime], [Guests], [Comments], [SittingId], [ReservationStatusId], [ReservationOriginId], [CustomerId]) VALUES (3, CAST(N'2022-07-13T18:30:00.0000000' AS DateTime2), 5, N'', 3, 3, 3, 6)
SET IDENTITY_INSERT [dbo].[Reservations] OFF
GO
SET IDENTITY_INSERT [dbo].[Areas] ON 

INSERT [dbo].[Areas] ([Id], [Name], [RestaurantId]) VALUES (1, N'Main', 1)
INSERT [dbo].[Areas] ([Id], [Name], [RestaurantId]) VALUES (2, N'Outside', 1)
INSERT [dbo].[Areas] ([Id], [Name], [RestaurantId]) VALUES (3, N'Balcony', 1)
SET IDENTITY_INSERT [dbo].[Areas] OFF
GO
SET IDENTITY_INSERT [dbo].[Tables] ON 

INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (1, N'M1', 5, 1)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (2, N'M2', 3, 1)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (3, N'M3', 4, 1)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (4, N'M4', 2, 1)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (5, N'M5', 5, 1)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (6, N'M6', 3, 1)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (7, N'M7', 3, 1)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (8, N'M8', 3, 1)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (9, N'M9', 3, 1)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (10, N'M10', 3, 1)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (11, N'O1', 3, 2)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (12, N'O2', 3, 2)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (13, N'O3', 3, 2)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (14, N'O4', 3, 2)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (15, N'O5', 3, 2)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (16, N'O6', 3, 2)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (17, N'O7', 3, 2)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (18, N'O8', 3, 2)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (19, N'O9', 3, 2)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (20, N'O10', 3, 2)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (21, N'B1', 3, 3)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (22, N'B2', 3, 3)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (23, N'B3', 3, 3)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (24, N'B4', 3, 3)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (25, N'B5', 3, 3)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (26, N'B6', 3, 3)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (27, N'B7', 3, 3)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (28, N'B8', 3, 3)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (29, N'B9', 3, 3)
INSERT [dbo].[Tables] ([Id], [TableName], [TableCapacity], [AreaId]) VALUES (30, N'B10', 3, 3)
SET IDENTITY_INSERT [dbo].[Tables] OFF
GO
INSERT [dbo].[ReservationTable] ([ReservationsId], [TablesId]) VALUES (1, 3)
INSERT [dbo].[ReservationTable] ([ReservationsId], [TablesId]) VALUES (1, 4)
INSERT [dbo].[ReservationTable] ([ReservationsId], [TablesId]) VALUES (3, 13)
INSERT [dbo].[ReservationTable] ([ReservationsId], [TablesId]) VALUES (3, 14)
INSERT [dbo].[ReservationTable] ([ReservationsId], [TablesId]) VALUES (3, 15)
INSERT [dbo].[ReservationTable] ([ReservationsId], [TablesId]) VALUES (2, 25)
GO

