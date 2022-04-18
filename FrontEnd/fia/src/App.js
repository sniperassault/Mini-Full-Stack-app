import React,{useState,useEffect} from 'react';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import {Modal,ModalBody,ModalFooter,ModalHeader} from 'reactstrap'
import Navbar from './Components/Navbar';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit, faTrashAlt } from '@fortawesome/free-solid-svg-icons';



function App() 
{
  const baseUrl="https://localhost:44347/api/WinnersEF";
  const [data,setData]=useState([]);
  const [modalInsertar,setModalInsertar]=useState(false);
  const [modalEditar,setModalEditar]=useState(false);
  const [modalEliminar,setModalEliminar]=useState(false);
  const [datos,setDatos]=useState(
    {     winnerID:'',
  
          driverID:'',
          name:'',
          lastname:'',
          age:'',
          country:'',
          titles:'',
     
          carID:'',
          model:'',
          manufacturer:'',
          year:'',
            
          trackID:'',
          trackName:'',
          location:'',
          length:'',
          capacity:'',
          turns:'',
          
          date:''
    }
  ) 


  const mapear= (winnerSeleccionado,opt)=>
  { 
    if(opt==1){
    setDatos(

      {
          winnerID:winnerSeleccionado.winnerID,
  
          driverID:winnerSeleccionado.driver.driverID,
          name:winnerSeleccionado.driver.name,
          lastname:winnerSeleccionado.driver.lastname,
          age:winnerSeleccionado.driver.age,
          country:winnerSeleccionado.driver.country,
          titles:winnerSeleccionado.driver.titles,
     
          carID:winnerSeleccionado.car.carID,
          model:winnerSeleccionado.car.model,
          manufacturer:winnerSeleccionado.car.manufacturer,
          year:winnerSeleccionado.car.year,
            
          trackID:winnerSeleccionado.track.trackID,
          trackName:winnerSeleccionado.track.trackName,
          location:winnerSeleccionado.track.location,
          length:winnerSeleccionado.track.length,
          capacity:winnerSeleccionado.track.capacity,
          turns:winnerSeleccionado.track.turns,
          date:winnerSeleccionado.date

      }
    )

    abrirCerrarModalEditar();}
    else
    {
      setDatos(

      {
      winnerID:winnerSeleccionado.winnerID})
      abrirCerrarModalEliminar();
    
    }

  }

  const handleInputChange = (event) => {
    setDatos({
        ...datos,
        [event.target.name] : event.target.value
    })}

  const limpiar=()=>
  { 
    setDatos({
    winnerID:'',
    driverID:'',
    carID:'',
    trackID:'',
    name:'',
    lastname:'',
    age:'',
    country:'',
    titles:'',
    model:'',
    manufacturer:'',
    year:'',
    trackName:'',
    location:'',
    length:'',
    capacity:'',
    turns:'',
    date:''})

  }

  
  const CreateWinnerBody=(datos,opt)=>
  {

    let body=
  {
    "winnerID": 0,
    "driver": {
      "driverID": 0,
      "name": "string",
      "lastname": "string",
      "age": 0,
      "country": "string",
      "titles": 0
    },
    "car": {
      "carID": 0,
      "model": "string",
      "manufacturer": "string",
      "year": 0
    },
    "track": {
      "trackID": 0,
      "trackName": "string",
      "location": "string",
      "length": 0,
      "capacity": 0,
      "turns": 0
    },
    "date": "2021-11-30T22:01:12.355Z"
  }
    
    if(opt===1){
    body.driver.name=datos.name;
    body.driver.lastname=datos.lastname;
    body.driver.age=datos.age;
    body.driver.country=datos.country;
    body.driver.titles=datos.titles;
    body.car.model=datos.model;
    body.car.manufacturer=datos.manufacturer;
    body.car.year=datos.year;
    body.track.trackName=datos.trackName;
    body.track.location=datos.location;
    body.track.length=datos.length;
    body.track.capacity=datos.capacity;
    body.track.turns=datos.turns;
    body.date=datos.date;}
    else{
    body.winnerID=datos.winnerID;
    body.driver.driverID=datos.driverID;
    body.car.carID=datos.carID;
    body.track.trackID=datos.trackID;
    body.driver.name=datos.name;
    body.driver.lastname=datos.lastname;
    body.driver.age=datos.age;
    body.driver.country=datos.country;
    body.driver.titles=datos.titles;
    body.car.model=datos.model;
    body.car.manufacturer=datos.manufacturer;
    body.car.year=datos.year;
    body.track.trackName=datos.trackName;
    body.track.location=datos.location;
    body.track.length=datos.length;
    body.track.capacity=datos.capacity;
    body.track.turns=datos.turns;
    body.date=datos.date
    }

    return body

  }
  
  const abrirCerrarModalInsertar=()=>{
    limpiar();
    setModalInsertar(!modalInsertar);
  }

  const abrirCerrarModalEditar=()=>{
     setModalEditar(!modalEditar);
  }

  const abrirCerrarModalEliminar=()=>{
    setModalEliminar(!modalEliminar);
  }

  const peticionGet=async()=>
  {
    await axios.get(baseUrl).then(response=>{setData(response.data);}).catch(error=>{console.log(error);})
  }

  const validar=(datos)=>
  {
    if ((datos.name=='')||(datos.lastname=='')||(datos.age=='')||(datos.country=='')||(datos.titles=='')||(datos.model=='')||(datos.manufacturer=='')||(datos.year=='')||(datos.trackName=='')||(datos.location=='')||(datos.length=='')||(datos.capacity=='')||(datos.turns==''))
    {return 1}
    else {return 0}
  }

  const peticionPost=async()=>
  {
   
    if (validar(datos)==0){
    await axios.post(baseUrl,CreateWinnerBody(datos,1)).then(response=>{abrirCerrarModalInsertar();peticionGet()}).catch(error=>{console.log(error);})
    }
    else {alert("Faltan completar datos!")}
  }

  const peticionPut=async()=>
  {
    
    if (validar(datos)==0){
    await axios.put((baseUrl+"/"+datos.winnerID),CreateWinnerBody(datos,2)).then(response=>{abrirCerrarModalEditar();peticionGet()}).catch(error=>{console.log(error);})
    }
    else {alert("Faltan completar datos!")}
  }

  const petitcionDelete=async()=>
  {
    await axios.delete((baseUrl+"/"+datos.winnerID)).then(response=>{abrirCerrarModalEliminar();peticionGet()}).catch(error=>{console.log(error);})
  }
 
//then(response=>{setData(data.concat(response.data));abrirCerrarModalInsertar();}).catch(error=>{console.log(error);})
  useEffect(()=>{peticionGet();},[])

  return (
    <div className="App">
   <Navbar/>
      <br/>
      <button onClick={()=>abrirCerrarModalInsertar()} className="btn btn-success">Agregar Ganador </button>
      <br/><br/>
      <table className="table table-bordered">
        <thead>
          <tr>
            <th class="text-primary">ID</th>
            <th class="text-secondary">Piloto</th>
            <th class="text-secondary">Edad</th>
            <th class="text-secondary"> Pais</th>
            <th class="text-secondary">Titulos</th>
            <th class="text-info">Vehiculo</th>
            <th class="text-dark">Circuito</th>
            <th class="text-dark">Locación</th>
            <th class="text-dark">Longitud</th>
            <th class="text-dark">Curvas</th>
            <th class="text-dark">Capacidad</th>
            <th class="text-success">Fecha de victoria</th>
          </tr>
        </thead>
        <tbody>
          {data.map(winner=>(
           <tr key={winner.winnerID}>
             <td>{winner.winnerID}</td>
             <td class="text-secondary">{winner.driver.name} {winner.driver.lastname}</td>
             <td class="text-secondary">{winner.driver.age}</td>
             <td class="text-secondary">{winner.driver.country}</td>
             <td class="text-secondary">{winner.driver.titles}</td>
             <td class="text-info">{winner.car.manufacturer} {winner.car.model} {winner.car.year}</td>
             <td class="text-dark">{winner.track.trackName}</td>
             <td class="text-dark">{winner.track.location}</td>
             <td class="text-dark">{winner.track.length}</td>
             <td class="text-dark">{winner.track.turns}</td>
             <td class="text-dark">{winner.track.capacity}</td>
             <td class="text-success">{winner.date}</td>
             <td>
               <button className="btn btn-primary" onClick={()=>mapear(winner,1)}><FontAwesomeIcon icon={faEdit}/></button> {" "}
               <button className="btn btn-primary" onClick={()=>mapear(winner,2)}><FontAwesomeIcon icon={faTrashAlt}/></button>
             </td>
             </tr>

          )
            )}
        </tbody>

      </table>
      
      <Modal isOpen={modalInsertar}>
      <ModalHeader>Agregar Ganador</ModalHeader>
      <ModalBody>
        <div className="form-group">
            <label>Nombre: </label>
            <br/>
            <input type="text" className="form-control" name="name" required onChange={handleInputChange}/>
            <label>Apellido: </label>
            <br/>
            <input type="text" className="form-control" name="lastname" required onChange={handleInputChange}/>
            <label>Edad: </label>
            <br/>
            <input type="number" min="10" max="200" step="1" className="form-control" required name="age" onChange={handleInputChange}/>
            <label>Pais: </label>
            <br/>
            <input type="text" className="form-control" name="country" required onChange={handleInputChange}/>
            <label>Titulos ganados: </label>
            <br/>
            <input type="number" className="form-control" required min="0" max="100" step="1" name="titles" onChange={handleInputChange}/>
            <label>Modelo Vehiculo: </label>
            <br/>
            <input type="text" className="form-control" required name="model" onChange={handleInputChange}/>
            <label>Constructor: </label>
            <br/>
            <input type="text" className="form-control" required name="manufacturer" onChange={handleInputChange}/>
            <label>año: </label>
            <br/>
            <input type="number" required min="1900" max="2100" step="1" className="form-control" name="year" onChange={handleInputChange}/>
            <label>Circuito: </label>
            <br/>
            <input type="text" required className="form-control" name="trackName" onChange={handleInputChange}/>
            <label>Locación: </label>
            <br/>
            <input type="text" required className="form-control" name="location" onChange={handleInputChange}/>
            <label>Longitud: </label>
            <br/>
            <input type="number" required min="100" max="30000" step="10"  className="form-control" name="length" onChange={handleInputChange}/>
            <label>Capacidad: </label>
            <br/>
            <input type="number" required min="1000" max="300000" step="100"  className="form-control" name="capacity" onChange={handleInputChange}/>
            <label>Curvas: </label>
            <br/>
            <input type="number" required min="1" max="100" step="1" className="form-control" name="turns" onChange={handleInputChange}/>
            <label>Fecha de victoria: </label>
            <br/>
            <input type="datetime-local" required className="form-control" name="date" onChange={handleInputChange}/>
        </div>

      </ModalBody>
      <ModalFooter>
        <button className="btn btn-primary" onClick={()=>peticionPost()}>Insertar</button>{"   "}
        <button className="btn btn-danger" onClick={()=>abrirCerrarModalInsertar()}>Cancelar</button>{"   "}
      </ModalFooter>
      </Modal>

      <Modal isOpen={modalEditar}>
      <ModalHeader>Editar Ganador</ModalHeader>
      <ModalBody>
        <div className="form-group">
            <label>Nombre: </label>
            <br/>
            <input type="text" className="form-control" name="name"   onChange={handleInputChange} value={ datos &&  datos.name}/>
            <label>Apellido: </label>
            <br/>
            <input type="text" className="form-control" name="lastname"   onChange={handleInputChange} value={ datos &&  datos.lastname}/>
            <label>Edad: </label>
            <br/>
            <input type="number" min="10" max="200" step="1" className="form-control"   name="age" onChange={handleInputChange} value={ datos.age}/>
            <label>Pais: </label>
            <br/>
            <input type="text" className="form-control" name="country"   onChange={handleInputChange} value={ datos &&  datos.country}/>
            <label>Titulos ganados: </label>
            <br/>
            <input type="number" className="form-control"   min="0" max="100" step="1" name="titles" onChange={handleInputChange} value={ datos &&  datos.titles}/>
            <label>Modelo Vehiculo: </label>
            <br/>
            <input type="text" className="form-control"   name="model" onChange={handleInputChange} value={ datos &&  datos.model}/>
            <label>Constructor: </label>
            <br/>
            <input type="text" className="form-control"   name="manufacturer" onChange={handleInputChange} value={ datos &&  datos.manufacturer}/>
            <label>año: </label>
            <br/>
            <input type="number"   min="1900" max="2100" step="1" className="form-control" name="year" onChange={handleInputChange} value={ datos &&  datos.year}/>
            <label>Circuito: </label>
            <br/>
            <input type="text"   className="form-control" name="trackName" onChange={handleInputChange} value={ datos &&  datos.trackName}/>
            <label>Locación: </label>
            <br/>
            <input type="text"   className="form-control" name="location" onChange={handleInputChange} value={ datos &&  datos.location}/>
            <label>Longitud: </label>
            <br/>
            <input type="number"   min="100" max="30000" step="10"  className="form-control" name="length" onChange={handleInputChange} value={ datos &&  datos.length}/>
            <label>Capacidad: </label>
            <br/>
            <input type="number"   min="1000" max="300000" step="100"  className="form-control" name="capacity" onChange={handleInputChange} value={ datos &&  datos.capacity}/>
            <label>Curvas: </label>
            <br/>
            <input type="number"   min="1" max="100" step="1" className="form-control" name="turns" onChange={handleInputChange} value={ datos &&  datos.turns}/>
            <label>Fecha de victoria: </label>
            <br/>
            <input type="datetime-local"   className="form-control" name="date" onChange={handleInputChange} value={ datos &&  datos.date}/>
            <br/>
            
        </div>

      </ModalBody>
      <ModalFooter>
        <button className="btn btn-primary" onClick={()=>peticionPut()}>Insertar</button>{"   "}
        <button className="btn btn-danger" onClick={()=>abrirCerrarModalEditar()}>Cancelar</button>{"   "}
      </ModalFooter>
      </Modal>
     
      <Modal isOpen={modalEliminar}>
      <ModalBody>
      ¿Seguro de que queres borrar al ganador #{datos && datos.winnerID}? 
      </ModalBody>
      <ModalFooter>
        <button className="btn btn-danger" onClick={()=>petitcionDelete()}>SI</button>
        <button className="btn btn-secondary" onClick={()=>abrirCerrarModalEliminar()}>NO</button>
      </ModalFooter>
      </Modal>


    </div>
  );
}

export default App;
