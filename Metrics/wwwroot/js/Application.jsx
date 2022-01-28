class Application extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            selectedMetrics: [],
            reportData: [],
            reportColumns: []
        };

        //because it's passed to the MetricSelect component
        this.setMetricSelectState = this.setMetricSelectState.bind(this);

        //because it's passed to the Refresh button
        this.getReportData = this.getReportData.bind(this);
    }

    //add/remove the id from this.state.selectedMetrics []
    setMetricSelectState(id, selected) {
        if (selected) {
            this.state.selectedMetrics.push(id);
        } else {
            let index = this.state.selectedMetrics.indexOf(id);
            if (index >= 0) {
                this.state.selectedMetrics.splice(index, 1);
            }
        }
    }

    getReportData() {
        fetch("/metricselect/getreportdata", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(this.state.selectedMetrics)
        })
            .then((response) => {
                response.json().then(reportData => {
                    console.log(reportData);
                    this.setState({ reportData: reportData.data, reportColumns: reportData.columns });
                });
            })
            .catch((error) => {
                console.error('Error:', error);
            });
    }

    render() {
        //making the result table just part of the application for now
        //if it gets any more complicated than plain data display, I would make a component for it
        return (
            <div className="Application">
                <div>
                    <MetricSelect setMetricSelectState={this.setMetricSelectState} />
                    <button type="button" onClick={this.getReportData}>Refresh</button>
                    <div className="ReportData">
                        Report Data
                        <table>
                            <thead>
                                <tr>
                                    {this.state.reportColumns.map(column =>
                                        <th key={column}>
                                            {column}
                                        </th>
                                    )}
                                </tr>
                            </thead>
                            <tbody>
                                {this.state.reportData.map((row, rowIndex) =>
                                    <tr key={rowIndex}>
                                        {row.map((cellData, cellIndex) =>
                                            <td key={cellIndex}>
                                                {cellData}
                                            </td>
                                        )}
                                    </tr>
                                )}
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        );
    }
}

ReactDOM.render(<Application />, document.getElementById('content'));
