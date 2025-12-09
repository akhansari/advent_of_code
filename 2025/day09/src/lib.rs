use std::collections::HashMap;

type Point = (usize, usize);
type Polygon = Vec<Point>;
type SegmentMap = HashMap<usize, Polygon>;

fn parse(input: &str) -> Polygon {
    input
        .lines()
        .map(|line| {
            let (x, y) = line.split_once(',').unwrap();
            (x.parse().unwrap(), y.parse().unwrap())
        })
        .collect()
}

pub fn part_one(input: &str) -> usize {
    let points = parse(input);
    let mut max_area = 0;
    for (i, &(x1, y1)) in points.iter().enumerate() {
        for &(x2, y2) in &points[i + 1..] {
            if x1 != x2 && y1 != y2 {
                let area = (x1.abs_diff(x2) + 1) * (y1.abs_diff(y2) + 1);
                max_area = max_area.max(area);
            }
        }
    }
    max_area
}

fn build_segments(polygon: &Polygon) -> (SegmentMap, SegmentMap) {
    let mut horizontal = HashMap::new();
    let mut vertical = HashMap::new();

    for i in 0..polygon.len() {
        let (x1, y1) = polygon[i];
        let (x2, y2) = polygon[(i + 1) % polygon.len()];

        if x1 == x2 {
            vertical
                .entry(x1)
                .or_insert_with(Vec::new)
                .push((y1.min(y2), y1.max(y2)));
        } else if y1 == y2 {
            horizontal
                .entry(y1)
                .or_insert_with(Vec::new)
                .push((x1.min(x2), x1.max(x2)));
        } else {
            panic!("should never happen based on constraints");
        }
    }

    (horizontal, vertical)
}

fn has_boundary_inside(
    min: Point,
    max: Point,
    horizontal: &SegmentMap,
    vertical: &SegmentMap,
) -> bool {
    for (&x, segs) in vertical {
        if x > min.0 && x < max.0 && segs.iter().any(|&(y1, y2)| y1 < max.1 && y2 > min.1) {
            return true;
        }
    }
    for (&y, segs) in horizontal {
        if y > min.1 && y < max.1 && segs.iter().any(|&(x1, x2)| x1 < max.0 && x2 > min.0) {
            return true;
        }
    }
    false
}

// Ray Casting Algorithm
fn point_in_polygon(point: Point, polygon: &Polygon) -> bool {
    fn to_isize(p: Point) -> (isize, isize) {
        (p.0 as isize, p.1 as isize)
    }
    let mut inside = false;
    let (px, py) = to_isize(point);

    for i in 0..polygon.len() {
        let (x1, y1) = to_isize(polygon[i]);
        let (x2, y2) = to_isize(polygon[(i + 1) % polygon.len()]); // next

        if x1 == x2 && px == x1 && py >= y1.min(y2) && py <= y1.max(y2)
            || y1 == y2 && py == y1 && px >= x1.min(x2) && px <= x1.max(x2)
        {
            return true; // point is on the edge = inside
        }

        // does horizontal ray cross this edge?
        // if number of crossings is odd, point is inside
        if (y1 > py) != (y2 > py) {
            let x_cross = x1 + (x2 - x1) * (py - y1) / (y2 - y1);
            if (px) < x_cross {
                inside = !inside;
            }
        }
    }
    inside
}

pub fn part_two(input: &str) -> usize {
    let polygon = parse(input);
    let (horizontal, vertical) = build_segments(&polygon);

    let mut max_area = 0;
    for (i, &(x1, y1)) in polygon.iter().enumerate() {
        for &(x2, y2) in &polygon[i + 1..] {
            if x1 == x2 || y1 == y2 {
                continue;
            }

            let min = (x1.min(x2), y1.min(y2));
            let max = (x1.max(x2), y1.max(y2));
            let area = (max.0 - min.0 + 1) * (max.1 - min.1 + 1);

            if area > max_area
                && !has_boundary_inside(min, max, &horizontal, &vertical)
                && point_in_polygon(((x1 + x2) / 2, (y1 + y2) / 2), &polygon)
            {
                max_area = area;
            }
        }
    }
    max_area
}
