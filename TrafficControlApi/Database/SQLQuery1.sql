--SELECT * FROM [dbo].[User] WHERE email='mb@wnb.dk' and pass='1234'
--INSERT [dbo].[User](email, pass, name) OUTPUT INSERTED.id VALUES('jn@wnb.dk', 'lol24', 'Jesper')
UPDATE [dbo].[User] SET email='jn@smsgate.dk', pass='lol24lol24', name='Jesper Nissen' WHERE id=2