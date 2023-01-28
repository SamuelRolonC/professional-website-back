dbWebsite = connect('mongodb://localhost:27017/website');

dbWebsite.createCollection("aboutMe");
dbWebsite.createCollection("blog");
dbWebsite.createCollection("contact");
dbWebsite.createCollection("project");
dbWebsite.createCollection("work");