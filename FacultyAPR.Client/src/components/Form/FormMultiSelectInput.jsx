import Form from 'react-bootstrap/Form'

const FormMultiSelectInput = ({onChange, content, options, isReadonly}) => {
  if (isReadonly)
  {
    return (
      <Form.Control readOnly defaultValue={content} as="select" size="sm"/>
    )
  }
  else
  {
    return (
    <Form.Control onChange={onChange} defaultValue={content} as="select" size="sm" custom>
      {options.map((item) => {
          return <option>{item}</option>
      })}
    </Form.Control>
    )
  }
}

export {FormMultiSelectInput};