// dbWebsite = connect('mongodb://localhost:27017/website');

db.work.insertMany(
[{
  "_id": new ObjectId(),
  "company": {
    "name": "HS Consulting",
    "url": "https://www.hsconsulting.com.ar",
    "location": "Ciudad Autónoma de Buenos Aires, Argentina"
  },
  "position": "Desarrollador Jr",
  "description": "I work in a reinsurance managing application, the main proyect of the company and in a printing insurance policy application.",
  "startDate": {
    "$date": {
      "$numberLong": "1580526000000"
    }
  },
  "isCurrentJob": true,
  "type": "Remote"
},{
  "_id": new ObjectId(),
  "company": {
    "name": "EDSI Trend Argentina",
    "url": "https://www.edsitrend.com/",
    "location": "Ciudad Autónoma de Buenos Aires, Argentina"
  },
  "position": "Technical Support",
  "description": "Technical support specialized in Trend Micro cyber security products. Tasks include implementation, migration and mainteinance of products.",
  "startDate": {
    "$date": {
      "$numberLong": "1533092400000"
    }
  },
  "endDate": {
    "$date": {
      "$numberLong": "1559358000000"
    }
  },
  "isCurrentJob": false,
  "type": "On site"
}]
)