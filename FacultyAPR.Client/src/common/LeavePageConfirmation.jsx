import Container from 'react-bootstrap/Container'
import { useEffect } from 'react';
import { Prompt} from 'react-router-dom'

const LeavePageConfirmation = (props) => {
    useEffect(() => {
      window.addEventListener('beforeunload', alertUser)
      return () => {
        window.removeEventListener('beforeunload', alertUser)
      }
    }, [])

    const alertUser = e => {
      e.preventDefault()
      e.returnValue = ''
    }
    
    return (
      <Container>
        <Prompt
          when={false}
          message={() => 'Are you sure you want to leave this page?'}
        />
      </Container>
    )
  }

  export default LeavePageConfirmation;
