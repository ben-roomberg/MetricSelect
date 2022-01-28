/* Expected properties
 * setMetricSelectState: function to set parent state. (id: number, state: boolean) => void;
 */
class MetricSelect extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            availableMetrics: [] //loaded inside componentDidMount
        };
        //because it's passed to the option element
        this.handleMetricClick = this.handleMetricClick.bind(this);
    }

    loadAvailableMetrics() {
        fetch("/metricselect/availablemetrics")
            .then((response) => {
                response.json().then(metrics => {
                    this.setState({ availableMetrics: metrics });
                });
            })
            .catch((error) => {
                console.error('Error:', error);
                //because there is no ui element to retry in the event of a server issue
                //and a full page reload is likely to be a problem
                //also, assumed to be replaced later with a proper stylized messaging system
                if (confirm('An error occured. Attempt to reload available metrics?')) {
                    this.loadAvailableMetrics();
                }
            });
    }

    componentDidMount() {
        this.loadAvailableMetrics();
    }


    handleMetricClick(event) {
        //normally a select will reset on each click unless holding ctrl
        //instead this will cause it to have toggleable options
        event.preventDefault();
        console.log(event.target);
        let newState = !event.target.getAttribute('selected');
        if (newState) {
            event.target.setAttribute('selected', 'selected');
        } else {
            event.target.removeAttribute('selected');
        }

        //call passed function from application parent to allow child to affect parent/sibling data
        this.props.setMetricSelectState(parseInt(event.target.getAttribute('value')), newState);
    }

    render() {
        return (
            <div className="MetricSelectContainer">
                Select your metrics
                <select size={this.state.availableMetrics.length} multiple="multiple" className="MetricSelect">
                    {this.state.availableMetrics.map(metric =>
                        <option key={metric.id} value={metric.id} onMouseDown={this.handleMetricClick}>
                            {metric.name}
                        </option>
                    )}
                </select>
            </div>
        );
    }
}

