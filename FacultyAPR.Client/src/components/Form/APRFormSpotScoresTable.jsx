import Table from 'react-bootstrap/Table'
import Container from 'react-bootstrap/Container'

export const APRFormSpotScoresTable = ({scores}) => {
    
    var courses = groupByCourse(scores);
    return (
        <Container>
        {Object.keys(courses).map(key =>
        {
            var course = courses[key];
            return(
                <Container>
                    <div>{course[0].course}</div>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                            <th>Question</th>
                            <th>Percent Respondents</th>
                            <th>Mean Score Value</th>
                            </tr>
                        </thead>
                        <tbody>
                        {courses[key].map(score => 
                        {   
                            return (
                            <tr>
                            <td>{score.question}</td>
                            <td>{score.percentRespondents}</td>
                            <td>{score.meanValue}</td>
                            </tr>
                            )
                        })}
                        </tbody>
                        </ Table>
                </Container>
                )
            })
        }
        </Container>
    );
}

const groupByCourse = (scores) => {
    var courses = {};
    scores.forEach(score => {
        if (!courses[score.course])
        {
            courses[score.course] = [];
        }
        courses[score.course].push(score)
    });

    return courses;
}
