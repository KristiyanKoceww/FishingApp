import React from 'react';

// const Fish = ({ name,weight,length,habittat,nutrition,description,tips,imageUrl }) => {
    const Fish = ({...props}) =>{
       console.log(props);
       
    return (

        props.map((x) =>(
            <div>
            <img alt="fish"  src={x.imageUrl} />
             <div>
              <span>Name: {x.name} -</span>
               <div>Description:{x.description}</div>
               <small>Weight:{x.weight} </small>
              <small>Length: {x.length}</small>
               <small>Habittat:{x.habittat} </small>
              <small>Nutrition:{x.nutrition} </small>
              <small>Tips:{x.tips} </small>
             </div>
          
           </div>
        ))
        // props.forEach(e => {
        //     <div >
        //     <img alt="fish"  src={e.imageUrl} />
        //     <div >
        //       <span>{e.name} -</span>
        //       <div>Description:{e.description}</div>
        //       <small>Weight:{e.weight} </small>
        //       <small>Length: {e.length}</small>
        //       <small>Habittat:{e.habittat} </small>
        //       <small>Nutrition:{e.nutrition} </small>
        //       <small>Tips:{e.tips} </small>
        //     </div>
           
        //   </div>
        // })
     

            
          
    


       
         
        // <div >
        //     <img alt="fish"  src={props.imageUrl} />
        //     <div >
        //       <span>{props.name} -</span>
        //       <div>Description:{props.description}</div>
        //       <small>Weight:{props.weight} </small>
        //       <small>Length: {props.length}</small>
        //       <small>Habittat:{props.habittat} </small>
        //       <small>Nutrition:{props.nutrition} </small>
        //       <small>Tips:{props.tips} </small>
        //     </div>
           
        //   </div>
    )
  }
  
  export default Fish;