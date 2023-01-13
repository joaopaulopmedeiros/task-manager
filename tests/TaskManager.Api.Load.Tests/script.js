import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    stages: [
        { duration: '1m', target: 100 }, // simulate ramp-up of traffic from 1 to 100 users over 5 minutes.
    ],
    thresholds: {
        'http_req_duration': ['p(99)<1500'], //99% of requests finished in less than 1.5 seconds
        'logged in successfully': ['p(99)<1500'],
    }
};

export default function () {
    const response = http.get('https://localhost:3333/tasks?size=1&page=1');

    check(response, {
        'response has status code 200': (x) => x.status === 200
    });

    sleep(1);
}