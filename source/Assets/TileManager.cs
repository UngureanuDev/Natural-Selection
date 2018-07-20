using UnityEngine;

public class TileManager : MonoBehaviour {

    [SerializeField]
    private Zone[] zoneArray;
    PhotonView photonView;

    private void Start()
    {
        //photonView = PhotonView.Get(this);
        zoneArray = GetComponentsInChildren<Zone>();
        initializeTiles();
    }
    private void initializeTiles()
    {
        photonView = PhotonView.Get(this);
        if (PhotonNetwork.player.IsMasterClient) //||True
        {
            for(int i = 0; i < zoneArray.Length; i++)
            {
                //int value = Random.Range(-4, 4);
                //int type =  Random.Range(0, 3);
                Debug.Log("Sending info to tile " + i);
                zoneArray[i].SetValues(i, 20);
                photonView.RPC("InitializeValue", PhotonTargets.Others, i, 20, i);
            }
        }
    }

    [PunRPC]
    void InitializeValue(int index, int value, int type)
    {
        Debug.Log("Received values " + index + " , " + value + " , " + type);
        zoneArray[index].SetValues(type, value);
    }

    public void RefreshValues()
    {
        for(int i = 0; i < zoneArray.Length; i++)
        {
            photonView = PhotonView.Get(this);
            photonView.RPC("RefreshValuesRPC", PhotonTargets.Others, i, zoneArray[i].value);
        }
    }
    [PunRPC]
    void RefreshValuesRPC(int index, int val)
    {
        zoneArray[index].UpdateValue(val);
    }


}