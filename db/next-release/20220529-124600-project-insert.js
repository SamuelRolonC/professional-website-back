dbWebsite = connect('mongodb://localhost:27017/website');

dbWebsite.project.insertMany({
    title: "Capacitar",
    description: "Digital therapy",
    url: "https://www.google.com",
    imagePath: "test.jpeg",
    languaje: "en"
},
{
    title: "Personal web site",
    description: "Web site with my professional information.",
    url: "https://www.google.com",
    imagePath: "",
    languaje: "en"
},
{
    title: "Capacitar",
    description: "Terapia digital",
    url: "https://www.google.com",
    imagePath: "test.jpeg",
    languaje: "es"
},
{
    title: "Sitio web personal",
    description: "Sitio web con mi información profesional.",
    url: "https://www.google.com",
    imagePath: "",
    languaje: "es"
});