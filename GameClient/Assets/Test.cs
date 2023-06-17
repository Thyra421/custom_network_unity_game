using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform sphere;

    public static bool IsCollisionDetected(
        Vector3 coneApex,
        float coneAngle,
        Vector3 coneDirection,
        float coneHeight,
        Vector3 sphereCenter,
        float sphereRadius) {
        // Calculate the vector from the cone apex to the sphere center
        Vector3 apexToSphere = sphereCenter - coneApex;

        // Calculate the distance from the cone apex to the sphere center along the cone direction
        float distanceAlongCone = Vector3.Dot(apexToSphere, coneDirection);

        // Check if the sphere center is behind the cone apex or beyond the cone height
        if (distanceAlongCone < 0 || distanceAlongCone > coneHeight) {
            // No collision as the sphere is outside the cone's height range
            return false;
        }

        // Calculate the radius of the cone at the distance along the cone
        float coneRadiusAtDistance = Mathf.Tan(Mathf.Deg2Rad * coneAngle) * distanceAlongCone;

        // Check if the sphere is within the cone's radius at the distance along the cone
        if (Vector3.Distance(sphereCenter, coneApex + distanceAlongCone * coneDirection) <= coneRadiusAtDistance + sphereRadius) {
            // Collision detected
            return true;
        }

        // No collision detected
        return false;
    }

    public static bool IsCollisionDetected(
        Vector3 sphere1Center,
        float sphere1Radius,
        Vector3 sphere2Center,
        float sphere2Radius) {
        float distance = Vector3.Distance(sphere1Center, sphere2Center);
        float combinedRadii = sphere1Radius + sphere2Radius;

        if (distance <= combinedRadii) {
            // Collision detected
            return true;
        }

        // No collision detected
        return false;
    }

    void Update() {
        //Debug.Log(IsCollisionDetected(new Vector3(0, 0, 0), 90, new Vector3(0, 0, 1), 2, sphere.transform.position, 1));

        Debug.Log(IsCollisionDetected(Vector3.zero, 1, sphere.transform.position, 1));

    }
}
