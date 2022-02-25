import {Button, Form} from 'react-bootstrap'
import  {CreateFormStructure} from '../../api/Routing.ts'
import {useState} from 'react';

const SchemaForm = () => {
    const [rank, setRank] = useState("")
    const [formYear, setFormYear] = useState("")
    const [schema, setSchema] = useState([]);
    
    const handleSubmit = async (event) => {
      event.preventDefault();
      CreateFormStructure(formYear, rank, schema.groups);
      console.log(schema.groups)
    };
    const handleRankChange = (event) => {
        setRank(event.target.value);
    };

    const handleYearChange = (event) => {
        setFormYear(event.target.value);
    };

    const handleSchemaChange = (event)  => {
        setSchema(event.target.value);
    };

    return (
        <Form onSubmit = {handleSubmit} >
        <Form.Group>
        <Form.Label>Faculty Rank</Form.Label>
            <Form.Control as="select" onChange = {handleRankChange}>
                <option>Professor</option>
                <option>Tenure</option>
            </Form.Control>
        </Form.Group>
       
        <Form.Group>
        <Form.Control type="text" placeholder="Form Year" onChange = {handleYearChange}/>
        </Form.Group>
      
        <Form.Group>
        <Form.Label>Form Schema</Form.Label>
            <Form.Control onChange ={handleSchemaChange} as="textarea" cols={20} rows={20} />
       </Form.Group>
        <Button className = "mt-2" variant="info" type="submit">
        Submit
        </Button>
    </Form>
);
}

export default SchemaForm