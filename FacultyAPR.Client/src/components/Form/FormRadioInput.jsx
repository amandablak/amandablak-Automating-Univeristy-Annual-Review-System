import Form from 'react-bootstrap/Form'

export const FormRadioInput = ({onChange, content, options, isReadonly, id}) => {
    return (
        <div key={`inline-radio`} className="mb-3">
            {
                options.map((item) => {
                    return <Form.Check
                            disabled={isReadonly}
                            name={`radios-${id}`}
                            label={item}
                            value={item}
                            checked={content === item}
                            type="radio"
                            onChange={onChange}
                        />
                    }
                )
            }
        </div>
    )
}