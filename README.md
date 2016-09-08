# Sqlite2CSfile
Generate C# class files from a sqlite db file.
## HOW TO USE
1. select a db file
2. select a dest fold,where you want the cs files be
3. type in database name
4. add interface if you want cs file to impl any.**NOTE:you have to do the impl your self,right now this is just for "empty interface"**
5. click generate btn,

**NOTE: right now, the parse function may be confusing.the parse function is pretty much a helloworld for my own deserialize test.if you have your own data read utils,just delete the parse function**

## Further Version
1. generate data source interface file,
2. better interface support,may not be very useful though
3. comment support,since sqlite has no comment.anyone have anyidea how to do this ?