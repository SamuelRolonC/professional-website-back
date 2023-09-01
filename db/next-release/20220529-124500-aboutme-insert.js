dbWebsite = connect('mongodb://localhost:27017/website');

dbWebsite.aboutMe.insertMany([
    {
        title: "I'm Samuel",
        text: "Lorem ipsum. Lorem ipsum. Salum.",
        language: "en"
    },
    {
        title: "Soy Samuel",
        text: "Lorem ipsum. Lorem ipsum. Salum.",
        language: "es"
    }
]);